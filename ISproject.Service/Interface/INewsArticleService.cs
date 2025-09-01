using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface INewsArticleService
    {
        NewsArticle Update(NewsArticle newsArticle);
        NewsArticle? Add(NewsArticle newsArticle);
        IEnumerable<NewsArticle?> AddAll(IEnumerable<NewsArticle> newsArticles);
        NewsArticle Delete(Guid id);

        NewsArticle? GetById(Guid id);
        NewsArticle? GetByCode(string code);
        IEnumerable<NewsArticle> GetAll();
        IEnumerable<BookmarkedArticle?> BookmarkArticles(string? userId, List<string> articleId);
    }
}
