# PostgreSQL configuration and migrations

The application and EF Core design-time factory both use PostgreSQL through Npgsql.
Development uses PostgreSQL too so schema changes are tested against the production provider.
The project remains on .NET / EF Core 9 for this database migration.

## Migration folders

- `ISproject.Repository/Data/Migrations/SqlServer`: original SQL Server migrations,
  designers, and model snapshot, preserved for reference. The repository project excludes
  these C# files from compilation; EF Core cannot discover or execute them. The archived
  snapshot filename is prefixed with `SqlServer` so EF tooling cannot overwrite it when
  locating the active `ApplicationDbContextModelSnapshot.cs`.
- `ISproject.Repository/Data/Migrations/PostgreSql`: active migrations and model snapshot.
  `InitialPostgreSql` creates the current model, including ASP.NET Identity tables.

EF discovers compiled migrations by context and assembly, rather than selecting a folder.
The shared `ApplicationDbContextConfiguration` explicitly selects the repository assembly,
which compiles only the PostgreSQL set. New migrations must use the output folder below.
Commit migrations, designers, and the snapshot together.

## Local database with Docker Compose

From the solution root, start PostgreSQL and wait for its health check:

```bash
docker compose -f docker-compose.db.yml up -d --wait
export ASPNETCORE_ENVIRONMENT=Development
dotnet tool restore
dotnet ef database update --project ISproject.Repository --startup-project ISproject.web --context ApplicationDbContext
```

Development settings already contain the connection string and local password that
match this Compose file. Run the application in the Development environment, which
is also set by the checked-in launch profiles. If a `ConnectionStrings__DefaultConnection`
environment variable is set in your terminal or IDE, it overrides these defaults;
remove it or include the correct password. The Compose file starts only the database;
it does not run the app or apply EF migrations. Its password is a local development default.

The database is bound to loopback and stored in the named `postgres_data` volume.
`docker compose -f docker-compose.db.yml down` stops it and preserves the data.

If port 5432 is already in use, start it with
`ISPROJECT_DB_PORT=8003 docker compose -f docker-compose.db.yml up -d --wait`
and change the application's connection string to `Port=8003`.

## Connection configuration

Development defaults to
`Host=localhost;Port=5432;Database=isproject;Username=isproject;Password=isproject-local`,
matching the local Compose database. Override it with an environment variable for both
the app and EF commands when using a different database. Set
`ASPNETCORE_ENVIRONMENT=Development` to use development settings.
Production has no default connection string and requires an explicit PostgreSQL connection.
Docker Compose supplies `Database__Host`, `Database__Port`, `Database__Name`,
`Database__Username`, and `Database__Password`; the shared configuration helper safely
builds the connection string from these settings. An explicit
`ConnectionStrings__DefaultConnection` takes precedence. See
[Docker deployment](docker-deployment.md) for the published migration image workflow.

```bash
# Example only: replace the host, database, username, and password for your environment.
export ConnectionStrings__DefaultConnection='Host=localhost;Port=5432;Database=isproject;Username=isproject;Password=replace-me'
export ASPNETCORE_ENVIRONMENT=Development
```

Provision a PostgreSQL database and user before deployment. For a remote database, follow
its TLS requirements (for example, `SSL Mode=VerifyFull` with the appropriate trusted CA).
Keep real credentials out of source control. The existing news API still requires `API_KEY`.

## Apply the committed migrations

Run these commands from the solution root. The local tool manifest pins `dotnet-ef` to 9.0.8.
The design-time factory reads web app settings from the solution root or web/repository
project directories and applies environment-variable overrides last.

```bash
dotnet tool restore
dotnet ef database update --project ISproject.Repository --startup-project ISproject.web --context ApplicationDbContext
```

For production, set `ASPNETCORE_ENVIRONMENT=Production` and supply the production
`ConnectionStrings__DefaultConnection` before executing the update command.
No database migration runs automatically on web application startup.

To generate reviewable PostgreSQL SQL instead of applying it immediately:

```bash
mkdir -p artifacts
dotnet ef migrations script --idempotent --project ISproject.Repository --startup-project ISproject.web --context ApplicationDbContext --output artifacts/postgresql-migrations.sql
```

## Future model changes

```bash
dotnet ef migrations add DescribeYourChange --project ISproject.Repository --startup-project ISproject.web --context ApplicationDbContext --output-dir Data/Migrations/PostgreSql --namespace ISproject.Repository.Data.Migrations.PostgreSql
dotnet ef migrations has-pending-model-changes --project ISproject.Repository --startup-project ISproject.web --context ApplicationDbContext
```

Inspect generated migrations before applying them. The initial migration targets a new
PostgreSQL database; it does not convert a SQL Server database or transfer Azure SQL data.
Do not copy SQL Server's `__EFMigrationsHistory` into PostgreSQL.

## Timestamps

Article publication, bookmark, and folder-entry timestamps use PostgreSQL
`timestamp with time zone` columns. New bookmark/folder timestamps are created with
`DateTime.UtcNow`; API publication offsets are converted to UTC. The context normalizes
local timestamps to UTC and treats offset-free datetime form inputs as UTC. Null values
remain null. PostgreSQL stores instants, rather than the original timezone or offset.
