using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface IArticleInFolderService
    {

        ArticleInFolder Add(ArticleInFolder articleInFolder);
        IEnumerable<ArticleInFolder> AddAll(IEnumerable<ArticleInFolder> articlesInFolder);
        ArticleInFolder Update(ArticleInFolder articleInFolder);
        ArticleInFolder? GetById(Guid id);
        ArticleInFolder Delete(Guid id);
        BookmarkFolder DeleteGrouped(Guid folderId);

        IEnumerable<ArticleInFolder> GetAll();
        IEnumerable<ArticleInFolder> AddToFolder(string? userId,string? folderObject, string? folderName, List<string> bookmarkIds);






    }
}
