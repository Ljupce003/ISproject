using ISproject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ISproject.web.Hosting;

public sealed class PostgreSqlHealthCheck(IServiceScopeFactory scopeFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var database = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;
        if (!await database.CanConnectAsync(cancellationToken))
        {
            return HealthCheckResult.Unhealthy("PostgreSQL is unavailable.");
        }
        if ((await database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            return HealthCheckResult.Unhealthy("Database migrations are pending.");
        }
        return HealthCheckResult.Healthy();
    }
}
