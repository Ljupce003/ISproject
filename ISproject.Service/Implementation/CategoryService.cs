using ISproject.Domain.Models;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;

namespace ISproject.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public Category Add(Category category)
        {
            return _repository.Insert(category);
        }

        public Category Delete(Guid id)
        {
            var category = GetById(id);
            if (category == null)
            {
                throw new ArgumentException("Category not found", nameof(id));
            }

            return _repository.Delete(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll(selector: c => c);
        }

        public Category? GetByCode(string code)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Code == code);
        }

        public Category? GetById(Guid id)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Id == id);
        }

        public Category Update(Category category)
        {
            return _repository.Update(category);
        }
    }
}
