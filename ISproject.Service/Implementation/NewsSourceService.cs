using ISproject.Domain.Models;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Service.Implementation
{
    public class NewsSourceService : INewsSourceService
    {
        private readonly IRepository<NewsSource> _repository;

        public NewsSourceService(IRepository<NewsSource> repository)
        {
            _repository = repository;
        }

        public NewsSource Add(NewsSource newsSource)
        {
            return _repository.Insert(newsSource);
        }

        public IEnumerable<NewsSource?> AddAll(IEnumerable<NewsSource> newsSources)
        {
            List<NewsSource> savedSources = [];

            foreach(var source in newsSources) {
                if ((source.Code != null && GetByCode(source.Code) == null) || source.Code == null) savedSources.Add(_repository.Insert(source));
            }

            return savedSources;
        }

        public NewsSource Delete(Guid id)
        {
            var newsSource = GetById(id);
            if (newsSource == null)
            {
                throw new ArgumentException("NewsSource not found", nameof(id));
            }

            return _repository.Delete(newsSource);
        }

        public IEnumerable<NewsSource> GetAll()
        {
            return _repository.GetAll(selector: c => c,
                include: ni => ni
                    .Include(n => n.Category!)
                    .Include(n => n.Country!)
                    .Include(n => n.Language!));
        }

        public NewsSource? GetByCode(string code)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Code == code, include: ni => ni
                    .Include(n => n.Category!)
                    .Include(n => n.Country!)
                    .Include(n => n.Language!));
        }

        public NewsSource? GetById(Guid id)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Id == id, include: ni => ni
                    .Include(n => n.Category!)
                    .Include(n => n.Country!)
                    .Include(n => n.Language!));

        }

        public NewsSource Update(NewsSource newsSource)
        {
            return _repository.Update(newsSource);
        }
    }
}
