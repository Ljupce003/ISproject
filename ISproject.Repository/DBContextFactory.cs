using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Repository;
using ISproject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Azure.Identity;
using Azure.Core;

namespace ISproject.Repository
{
    public class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

//namespace ISproject.Repository
//{
//    public class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            // Get connection string without authentication info
//            var connectionString = "Server=tcp:isproject221563.database.windows.net,1433;Database=ISproject.web_db;Encrypt=True;TrustServerCertificate=False;";

//            // Create SQL connection and set AccessToken for Azure AD authentication
//            var sqlConnection = new SqlConnection(connectionString);
//            var tokenCredential = new DefaultAzureCredential();
//            sqlConnection.AccessToken = tokenCredential.GetToken(
//                new TokenRequestContext(new[] { "https://database.windows.net/.default" })
//            ).Token;

//            // Configure DbContext options
//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            optionsBuilder.UseSqlServer(sqlConnection);

//            return new ApplicationDbContext(optionsBuilder.Options);
//        }
//    }
//}



//namespace ISproject.Repository
//{
//    public class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            // Load configuration
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
//                .AddJsonFile("appsettings.json", optional: true)
//                .AddEnvironmentVariables()
//                .Build();

//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

//            // Try Azure connection string first
//            var azureConnectionString = configuration["AZURE_SQL_CONNECTIONSTRING"];

//            if (!string.IsNullOrEmpty(azureConnectionString))
//            {
//                var conn = new SqlConnection(azureConnectionString);

//                // Acquire token for Entra login
//                var tokenCredential = new DefaultAzureCredential();
//                AccessToken token = tokenCredential.GetToken(
//                    new TokenRequestContext(new[] { "https://database.windows.net/.default" })
//                );

//                conn.AccessToken = token.Token;
//                optionsBuilder.UseSqlServer(conn);
//            }
//            else
//            {
//                // Fallback to local DB
//                var localConnectionString = configuration.GetConnectionString("DefaultConnection");
//                optionsBuilder.UseSqlServer(localConnectionString);
//            }

//            return new ApplicationDbContext(optionsBuilder.Options);
//        }
//    }
//}

