using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface ICategoryService
    {
        Category Update(Category category);
        Category Add(Category category);
        Category Delete(Guid id);

        Category? GetById(Guid id);
        Category? GetByCode(string code);
        IEnumerable<Category> GetAll();
    }
}
