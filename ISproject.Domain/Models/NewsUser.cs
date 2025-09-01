using Microsoft.AspNetCore.Identity;

namespace ISproject.Domain.Models
{
    public class NewsUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        public BookmarkCart? BookmarkCart { get; set; }
        public Guid? BookmarkCartId { get; set; }

        public virtual ICollection<BookmarkFolder>? BookmarkFolders { get; set; }
    }
}
