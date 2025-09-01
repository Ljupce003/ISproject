using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ISproject.Domain.Models;


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

    }
}
