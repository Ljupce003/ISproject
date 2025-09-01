namespace ISproject.Domain.Models.dto
{
    public class NewsArticleDTO
    {
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Source { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public string? Language { get; set; }
        public string? Country { get; set; }
        public DateTime? Published_at { get; set; }
    }
}
