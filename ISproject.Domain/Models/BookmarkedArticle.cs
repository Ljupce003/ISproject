namespace ISproject.Domain.Models
{
    public class BookmarkedArticle : BaseModel
    {
        public NewsArticle? Article { get; set; }
        public Guid? ArticleId { get; set; }
        public BookmarkCart? BookmarkCart { get; set; }
        public Guid? BookmarkCartId { get; set; }
        public DateTime? AddedAt { get; set; }
    }
}
