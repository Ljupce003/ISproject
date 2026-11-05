# Build, publish, and deploy ISproject

The Dockerfile produces two images from the same source revision:

- `runtime`: the ASP.NET Core web application, running as the image's non-root `app` user.
- `migrations`: a self-contained EF Core migration bundle, also running as `app`.

Production uses published images, PostgreSQL 17, and named volumes for the database and
ASP.NET Identity Data Protection keys. The VM needs Docker with Compose v2, not .NET or
application source code. Nginx terminates HTTPS and forwards HTTP to app port 8001.
The application retains .NET 9; plan its runtime upgrade separately.

## Build and publish

On your development/build machine, from the solution root:

```bash
cp .env.build.example .env.build
# Edit both image names with your registry namespace and a matching release tag.
# Set BUILD_PLATFORM=linux/amd64 for an x86_64 VM or linux/arm64 for an ARM VM.
docker compose --env-file .env.build -f docker-compose.build.yml build --pull
docker login
docker compose --env-file .env.build -f docker-compose.build.yml push
```

Check the VM's `uname -m`: `x86_64` corresponds to `linux/amd64`; `aarch64` corresponds
to `linux/arm64`. Your Mac's CPU does not determine the VM image architecture.
A build targeting a different CPU uses Docker's emulation support and may take longer.
Keep both images on the same release tag and source revision. Avoid mutable `latest` tags
for deployments that need reproducible rollback. Production credentials are not build inputs.

If you need a single tag that supports both CPU architectures, use Buildx and publish
both targets (replace the example registry namespace/tag):

```bash
docker buildx build --platform linux/amd64,linux/arm64 --target runtime --tag your-dockerhub-user/isproject-app:1.0.0 --push .
docker buildx build --platform linux/amd64,linux/arm64 --target migrations --tag your-dockerhub-user/isproject-migrations:1.0.0 --push .
```

## Prepare the VM

Copy these files into one deployment directory, for example `/opt/isproject`:

- `docker-compose.prod.yml`
- `docker-compose.native.yml` for host-installed Nginx, or `docker-compose.proxy.yml` for Docker Nginx
- `compose-prod.sh`
- `.env.production.example`

```bash
cd /opt/isproject
cp .env.production.example .env.production
chmod 600 .env.production
chmod +x compose-prod.sh
# Edit .env.production before continuing.
```

Set the published `APP_IMAGE` and `MIGRATION_IMAGE`, public domain `APP_HOST`, news
`API_KEY`, and database credentials. Generate a database password with `openssl rand -hex 32`.
Both the app and migration bundle receive structured `Database__*` settings, so password
characters are escaped correctly in the generated PostgreSQL connection string.
The app receives `API_KEY` directly and does not look for a production `.env` inside the image.

The production stack has its own project name and volumes, separate from the local
`docker-compose.db.yml` database. It does not import local or Azure data automatically.

### Host-installed Nginx (default)

`./compose-prod.sh` adds `docker-compose.native.yml`, publishes the application on
`127.0.0.1:8001`, and loads `.env.production` for Compose interpolation. The existing
nginx-reverse-proxy project already expects `ISPROJECT_UPSTREAM=http://127.0.0.1:8001`.

The default application subnet is `172.30.81.0/24`; its bridge gateway is `172.30.81.1`.
The environment example trusts this gateway as `TRUSTED_PROXY_CIDRS=172.30.81.1/32`.
Host-installed Nginx normally connects from that gateway. If the subnet conflicts with
another network, change `APP_NETWORK_SUBNET` and update the trusted gateway CIDR too.
After starting PostgreSQL, confirm the gateway:

```bash
docker network inspect isproject-production_default --format '{{(index .IPAM.Config 0).Gateway}}'
```

Configure DNS, issue the ISproject certificate, and install its native Nginx virtual host
using the existing nginx-reverse-proxy project's native scripts. `APP_HOST` must match
`ISPROJECT_DOMAIN` there. HTTPS handling needs Nginx's `X-Forwarded-For`,
`X-Forwarded-Proto`, and `X-Forwarded-Host` headers from a configured trusted proxy.

### Docker Nginx (alternative)

Use `./compose-prod.sh --proxy` for every deployment command instead. The web container
joins the existing external `web-proxy` network with alias `isproject-web`; it publishes
no HTTP port on the VM. PostgreSQL and the migration runner stay on the app network.

```bash
docker network inspect web-proxy --format '{{range .IPAM.Config}}{{.Subnet}}{{end}}'
```

Set `TRUSTED_PROXY_CIDRS` to the Nginx container's IP with `/32` or to this subnet if
all peers on it are trusted. Multiple CIDRs are comma-separated. The shared Nginx
configuration already routes to `http://isproject-web:8001`. The external network must
exist before starting the web container.

## Deploy

For native Nginx:

```bash
./compose-prod.sh pull postgres web migrate
./compose-prod.sh up -d --wait postgres
./compose-prod.sh run --rm migrate
./compose-prod.sh up -d --wait web
./compose-prod.sh ps
curl --fail http://127.0.0.1:8001/health
curl --fail https://isproject.example.com/health
```

Use `./compose-prod.sh --proxy` in place of `./compose-prod.sh` for Docker Nginx;
verify the public HTTPS health URL instead of the unpublished loopback app port.
Stop the deployment if the migration command fails. The migration runner exits after
applying pending migrations; running it again on an up-to-date database is safe.
The web app never applies migrations on startup. `/health` returns 503 if PostgreSQL
is unavailable or migrations are pending, and 200 when the database is ready.

Database access from the VM or an SSH tunnel is available at `127.0.0.1:8003`.
Application containers connect to `postgres:5432`, not that host port.
Use the default HTTP port 8001 unless you update Nginx's upstream to match a different
`ISPROJECT_HTTP_PORT`. The container listener remains 8001.

For logs:

```bash
./compose-prod.sh logs --tail 100 web
./compose-prod.sh logs --tail 100 postgres
```

`./compose-prod.sh down` preserves the named volumes. Do not use `down --volumes`
when you need to retain data or login encryption keys.

## Update and rollback

Publish both images with a new matching tag, then update their names in `.env.production`.
Back up the database and Data Protection key volume first. For a simple maintenance-window
update, stop the existing web container before changing the schema:

```bash
./compose-prod.sh pull web migrate
./compose-prod.sh stop web
./compose-prod.sh run --rm migrate
./compose-prod.sh up -d --wait web
```

A successful image rollback requires a schema compatible with the older application.
Changing image tags does not undo migrations. Restore a tested backup when the previous
application cannot run against the new schema. Changing DB_PASSWORD in the environment
file alone does not change an existing PostgreSQL user's password.

## Backups

Create a logical PostgreSQL backup outside the containers:

```bash
mkdir -p backups
chmod 700 backups
./compose-prod.sh exec -T postgres sh -c 'pg_dump -U "$POSTGRES_USER" -d "$POSTGRES_DB" -Fc' > backups/isproject.dump
```

Keep off-VM copies and test restores into a separate database. Also back up
`isproject-production_data_protection_keys`; preserving that volume keeps existing login
cookies decryptable across web container recreation. Limit access to the database and
key backups. The production environment file and backups must stay out of version control.
