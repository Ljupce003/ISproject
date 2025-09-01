using Microsoft.EntityFrameworkCore;

namespace ISproject.Domain.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class NewsArticle : BaseModel
    {
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Source { get; set; }

        //public NewsSource? Source { get; set; }
        //public Guid? SourceId { get; set; }

        public string? ImageUrl { get; set; }

        public Category? Category { get; set; }
        public Guid? CategoryId { get; set; }

        public Language? Language { get; set; }
        public Guid? LanguageId { get; set; }

        public Country? Country { get; set; }
        public Guid? CountryId { get; set; }

        public DateTime? PublishedAt { get; set; }

        public string? Code { get; set; }

        public virtual IEnumerable<BookmarkedArticle>? ArticleInCarts { get; set; }

    }
}
