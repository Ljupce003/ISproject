using ISproject.Domain.Models;
using ISproject.Repository.Data;
using ISproject.Repository.Implementation;
using ISproject.Repository.Interface;
using ISproject.Service.Implementation;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using ISproject.web.Hosting;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Local convenience only; deployed containers receive configuration from Compose.
if (builder.Environment.IsDevelopment())
{
    var envPath = Path.Combine(builder.Environment.ContentRootPath, "..", ".env");
    if (File.Exists(envPath))
    {
        Env.NoClobber().Load(envPath);
        builder.Configuration.AddEnvironmentVariables();
    }
}

ReverseProxyConfiguration.Configure(builder.Services, builder.Configuration);
var dataProtection = builder.Services.AddDataProtection().SetApplicationName("ISproject");
var keysPath = builder.Configuration["DataProtection:KeysPath"];
if (!string.IsNullOrWhiteSpace(keysPath))
{
    Directory.CreateDirectory(keysPath);
    dataProtection.PersistKeysToFileSystem(new DirectoryInfo(keysPath));
}

// Runtime and EF tooling use the same PostgreSQL provider and migrations assembly.
var connectionString = ApplicationDbContextConfiguration.GetConnectionString(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    ApplicationDbContextConfiguration.ConfigurePostgreSql(options, connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHealthChecks().AddCheck<PostgreSqlHealthCheck>("postgresql");

builder.Services.AddDefaultIdentity<NewsUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options => options.HttpsPort = 443);
    builder.Services.ConfigureApplicationCookie(options =>
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always);
}
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
app.UseForwardedHeaders();

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

// Docker checks the application directly over HTTP; public traffic uses Nginx HTTPS.
app.UseWhen(context => !context.Request.Path.Equals("/health"),
    branch => branch.UseHttpsRedirection());
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health").AllowAnonymous();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
