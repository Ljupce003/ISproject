using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface ICountryService
    {
        Country Update(Country country);
        Country Add(Country country);
        IEnumerable<Country> AddAll(IEnumerable<Country> countries);
        Country Delete(Guid id);

        Country? GetById(Guid id);
        Country? GetByCode(string code);
        IEnumerable<Country> GetAll();


    }
}
