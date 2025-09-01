using ISproject.Domain.Models;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;

namespace ISproject.Service.Implementation
{
    public class LanguageService : ILanguageService
    {
        private readonly IRepository<Language> _repository;

        public LanguageService(IRepository<Language> repository)
        {
            _repository = repository;
        }

        public Language Add(Language language)
        {
            return _repository.Insert(language);
        }

        public Language Delete(Guid id)
        {
            var language = GetById(id);
            if (language == null)
            {
                throw new ArgumentException("language not found", nameof(id));
            }

            return _repository.Delete(language);
        }

        public IEnumerable<Language> GetAll()
        {
            return _repository.GetAll(selector: c => c);
        }

        public Language? GetByCode(string code)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Code == code);
        }

        public Language? GetById(Guid id)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Id == id);

        }

        public Language Update(Language language)
        {
            return _repository.Update(language);
        }
    }
}
