using ISproject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ISproject.Repository;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Production";

        // Support commands from the solution, web, repository, or published directory.
        var currentDirectory = Directory.GetCurrentDirectory();
        var candidates = new[]
        {
            currentDirectory,
            Path.Combine(currentDirectory, "ISproject.web"),
            Path.GetFullPath(Path.Combine(currentDirectory, "..", "ISproject.web")),
            AppContext.BaseDirectory
        };
        var configurationDirectory = candidates.FirstOrDefault(directory =>
            File.Exists(Path.Combine(directory, "appsettings.json"))) ?? currentDirectory;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(configurationDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        ApplicationDbContextConfiguration.ConfigurePostgreSql(
            options, ApplicationDbContextConfiguration.GetConnectionString(configuration));

        return new ApplicationDbContext(options.Options);
    }
}
