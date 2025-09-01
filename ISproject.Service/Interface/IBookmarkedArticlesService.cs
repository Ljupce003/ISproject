using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface IBookmarkedArticlesService
    {
        BookmarkedArticle Add(BookmarkedArticle article);
        IEnumerable<BookmarkedArticle> AddAll(IEnumerable<BookmarkedArticle> articles);
        BookmarkedArticle Update(BookmarkedArticle article);
        BookmarkedArticle Delete(Guid Id);

        BookmarkedArticle? GetById(Guid Id);
        IEnumerable<BookmarkedArticle> GetAll();
        IEnumerable<BookmarkedArticle> GetAllbyUser(string? userId);
    }
}
