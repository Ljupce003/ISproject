using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;

namespace ISproject.Service.Interface
{
    public interface IBookmarkCartService
    {
        BookmarkCart Add(BookmarkCart bookmarkCart);

        BookmarkCart GetByUserId(string? userId);
        BookmarkCart? GetById(Guid id);
    }
}
