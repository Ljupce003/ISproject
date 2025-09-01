using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface ILanguageService
    {
        Language Update(Language language);
        Language Add(Language language);
        Language Delete(Guid id);

        Language? GetById(Guid id);
        Language? GetByCode(string code);
        IEnumerable<Language> GetAll();
    }
}
