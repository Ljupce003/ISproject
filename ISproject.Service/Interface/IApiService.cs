using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface IApiService
    {
        Task<IEnumerable<NewsArticle>?> GetNewsArticles(
            string? countries,
            string? categories,
            string? languages,
            string? sources,
            string? sort,
            DateTime? dateTime,
            int? limit = 25,
            int? offset = 0);


        Task<IEnumerable<NewsSource>?> GetNewsSources(
            string searchKeyword,
            string? countries,
            string? categories,
            string? languages,
            int? limit = 25,
            int? offset = 0);
    }
}
