using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface INewsSourceService
    {
        NewsSource Update(NewsSource newsSource);
        NewsSource Add(NewsSource newsSource);
        IEnumerable<NewsSource?> AddAll(IEnumerable<NewsSource> newsSources);
        NewsSource Delete(Guid id);

        NewsSource? GetById(Guid id);
        NewsSource? GetByCode(string code);
        IEnumerable<NewsSource> GetAll();
    }
}
