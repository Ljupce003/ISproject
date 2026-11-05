# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:9.0-bookworm-slim AS build
WORKDIR /source
COPY ISproject.sln ./
COPY .config/dotnet-tools.json .config/dotnet-tools.json
COPY ISproject.Domain/ISproject.Domain.csproj ISproject.Domain/
COPY ISproject.Repository/ISproject.Repository.csproj ISproject.Repository/
COPY ISproject.Service/ISproject.Service.csproj ISproject.Service/
COPY ISproject.web/ISproject.web.csproj ISproject.web/
RUN dotnet restore ISproject.sln
COPY . .
RUN dotnet publish ISproject.web/ISproject.web.csproj -c Release --no-restore \
    -p:UseAppHost=false -o /out/web

FROM build AS migration-build
ARG TARGETARCH
RUN dotnet tool restore \
    && case "$TARGETARCH" in \
        amd64) migration_rid=linux-x64 ;; \
        arm64) migration_rid=linux-arm64 ;; \
        *) echo "Unsupported migration architecture: $TARGETARCH" >&2; exit 1 ;; \
    esac \
    && ConnectionStrings__DefaultConnection='Host=localhost;Database=isproject;Username=isproject' \
       dotnet ef migrations bundle --project ISproject.Repository \
       --startup-project ISproject.web --context ApplicationDbContext \
       --configuration Release --no-build --self-contained \
       --target-runtime "$migration_rid" --output /out/migrate/efbundle

FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim AS runtime-base
WORKDIR /app
ENV DOTNET_ENVIRONMENT=Production \
    ASPNETCORE_ENVIRONMENT=Production \
    ASPNETCORE_URLS=http://+:8001

FROM runtime-base AS migrations
COPY --from=migration-build --chmod=755 /out/migrate/efbundle ./efbundle
COPY --from=build /out/web/appsettings.json ./appsettings.json
USER $APP_UID
ENTRYPOINT ["./efbundle"]

FROM runtime-base AS runtime
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/* \
    && mkdir -p /var/lib/isproject/keys \
    && chown -R "$APP_UID:$APP_UID" /var/lib/isproject
ENV DataProtection__KeysPath=/var/lib/isproject/keys
COPY --from=build /out/web ./
# Local database credentials have no place in the production image.
RUN rm -f appsettings.Development.json
USER $APP_UID
EXPOSE 8001
ENTRYPOINT ["dotnet", "ISproject.web.dll"]
