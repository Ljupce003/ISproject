using System.ComponentModel.DataAnnotations.Schema;

namespace ISproject.Domain.Models
{
    public class BookmarkCart : BaseModel
    {
        public NewsUser? User { get; set; }
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public virtual IEnumerable<BookmarkedArticle>? ArticlesInCart { get; set; }
    }
}
