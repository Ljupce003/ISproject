using Azure.Identity;
using ISproject.Domain.Models;
using ISproject.Repository.Data;
using ISproject.Repository.Implementation;
using ISproject.Repository.Interface;
using ISproject.Service.Implementation;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DotNetEnv;

// Load .env file
var envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", ".env");
Env.Load(envPath);


var builder = WebApplication.CreateBuilder(args);


//var azureConnectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
//var localConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    if (!string.IsNullOrEmpty(azureConnectionString))
//    {
//        // Azure SQL with Entra login
//        var conn = new SqlConnection(azureConnectionString);

//        // Get token for Entra login
//        var tokenCredential = new DefaultAzureCredential();
//        conn.AccessToken = tokenCredential.GetToken(
//            new Azure.Core.TokenRequestContext(
//                new[] { "https://database.windows.net/.default" }
//            )
//        ).Token;

//        options.UseSqlServer(conn);
//    }
//    else
//    {
//        // Local fallback
//        options.UseSqlServer(localConnectionString);
//    }
//});

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Exception filter for easier debugging
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(connectionString)
    
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,                  // retry 5 times
            maxRetryDelay: TimeSpan.FromSeconds(15),  // wait up to 15s between retries
            errorNumbersToAdd: null
        );
    })
    
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<NewsUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ILanguageService, LanguageService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<INewsArticleService, NewsArticleService>();
builder.Services.AddTransient<INewsSourceService, NewsSourceService>();
builder.Services.AddTransient<IBookmarkCartService, BookmarkCartService>();
builder.Services.AddTransient<IBookmarkedArticlesService, BookmarkedArticlesService>();
builder.Services.AddTransient<IBookMarkFolderService, BookMarkFolderService>();
builder.Services.AddTransient<IArticleInFolderService, ArticleInFolderService>();
builder.Services.AddTransient<IApiService, ApiService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
