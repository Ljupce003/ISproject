using ISproject.Domain.Models.dto;

namespace ISproject.Domain.Models.Responses
{
    public class NewsArticleResponse
    {
        public Pagination? Pagination { get; set; }
        public IEnumerable<NewsArticleDTO>? Data { get; set; }
    }
}
