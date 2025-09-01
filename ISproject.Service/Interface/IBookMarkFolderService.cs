using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface IBookMarkFolderService
    {
        IEnumerable<BookmarkFolder> GetFoldersByUserId(string? userId);
        IEnumerable<BookmarkFolder> GetFolderNamesByUserId(string? userId);

        BookmarkFolder? GetById(Guid id);
        BookmarkFolder? GetByIdNoData(Guid id);
        BookmarkFolder? GetByIdFullData(Guid id);

        IEnumerable<BookmarkFolder> GetAll();

        BookmarkFolder Update(BookmarkFolder folder);
        BookmarkFolder Add(BookmarkFolder folder);
        BookmarkFolder Delete(Guid id);
        byte[] ExportFileToCSV(Guid folderId);

    }
}
