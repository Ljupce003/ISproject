using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ISproject.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace ISproject.Repository.Data
{
    
    public class ApplicationDbContext : IdentityDbContext <NewsUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<NewsSource> NewsSources { get; set; }
        public DbSet<BookmarkCart> BookmarkCarts { get; set; }
        public DbSet<BookmarkedArticle> ArticleInBookMarkCarts { get; set; }
        public DbSet<BookmarkFolder> BookmarkFolders { get; set; }
        public DbSet<ArticleInFolder> ArticlesInFolder { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // HTML datetime inputs have no offset; the application's input convention is UTC.
            // Normalize local values before PostgreSQL writes timestamp with time zone columns.
            var utcTimestamp = new ValueConverter<DateTime, DateTime>(
                value => value.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                    : value.ToUniversalTime(),
                value => DateTime.SpecifyKind(value, DateTimeKind.Utc));

            builder.Entity<NewsArticle>().Property(article => article.PublishedAt)
                .HasConversion(utcTimestamp);
            builder.Entity<BookmarkedArticle>().Property(article => article.AddedAt)
                .HasConversion(utcTimestamp);
            builder.Entity<ArticleInFolder>().Property(article => article.DateAdded)
                .HasConversion(utcTimestamp);
        }

    }
}
