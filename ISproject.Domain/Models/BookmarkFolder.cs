using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISproject.Domain.Models
{
    public class BookmarkFolder: BaseModel
    {
        public string? Name { get; set; }

        public NewsUser? User { get; set;  }
        public string? UserId { get; set;  }

        public virtual ICollection<ArticleInFolder>? ArticleInFolders { get; set; }

    }
}
