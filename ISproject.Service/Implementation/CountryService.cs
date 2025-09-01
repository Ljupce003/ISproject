using ISproject.Domain.Models;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;

namespace ISproject.Service.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _repository;

        public CountryService(IRepository<Country> repository)
        {
            _repository = repository;
        }

        public Country Add(Country country)
        {
            return _repository.Insert(country);
        }

        public IEnumerable<Country> AddAll(IEnumerable<Country> countries)
        {
            //var countryList = countries.ToList();
            List<Country> saveList = [];

            foreach (var country in countries.ToList()) {
                if (GetByCode(country.Code!) == null) saveList.Add(country);
            }

            return _repository.InsertMany(saveList);
        }

        public Country Delete(Guid id)
        {
            var country = GetById(id);
            if (country == null)
            {
                throw new ArgumentException("Country not found", nameof(id));
            }

            return _repository.Delete(country);
        }

        public IEnumerable<Country> GetAll()
        {
            return _repository.GetAll(selector: c => c);
        }

        public Country? GetByCode(string code)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Code == code);
        }

        public Country? GetById(Guid id)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Id == id);
        }

        public Country Update(Country country)
        {
            return _repository.Update(country);
        }
    }
}
