using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISproject.Domain.Models
{
    public class ArticleInFolder: BaseModel
    {
        public NewsArticle? NewsArticle { get; set; }
        public Guid? NewsArticleId { get; set; }

        public BookmarkFolder? BookmarkFolder { get; set; }
        public Guid? BookmarkFolderId { get; set; }

        public DateTime? DateAdded { get; set; }

    }
}
