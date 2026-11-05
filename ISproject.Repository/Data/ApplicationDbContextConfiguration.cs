using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ISproject.Repository.Data;

public static class ApplicationDbContextConfiguration
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            return connectionString;
        }

        string Required(string key) => !string.IsNullOrWhiteSpace(configuration[key])
            ? configuration[key]!
            : throw new InvalidOperationException(
                $"Set ConnectionStrings__DefaultConnection or supply {key.Replace(":", "__")}.");

        // Build the string safely even when a password contains delimiters.
        return new NpgsqlConnectionStringBuilder
        {
            Host = Required("Database:Host"),
            Port = configuration.GetValue("Database:Port", 5432),
            Database = Required("Database:Name"),
            Username = Required("Database:Username"),
            Password = Required("Database:Password")
        }.ConnectionString;
    }

    public static void ConfigurePostgreSql(DbContextOptionsBuilder options, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Set ConnectionStrings__DefaultConnection to a PostgreSQL connection string.");
        }

        options.UseNpgsql(connectionString, postgresOptions =>
        {
            // Only the PostgreSQL migrations are compiled into this assembly.
            postgresOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name);
            postgresOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(15), null);
        });
    }
}
