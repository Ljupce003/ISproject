using ISproject.Domain.Models.dto;

namespace ISproject.Domain.Models.Responses
{
    public class NewsSourceResponse
    {
        public Pagination? pagination { get; set; }
        
        public IEnumerable<NewsSourceDTO>? data { get; set; }
    }
}
