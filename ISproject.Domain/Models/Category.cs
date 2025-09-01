using Microsoft.EntityFrameworkCore;

namespace ISproject.Domain.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Category : BaseModel
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
