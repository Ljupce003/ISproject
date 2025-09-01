using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Domain.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class NewsSource : BaseModel
    {
        [Required]
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }

        public Category? Category { get; set; }
        public Guid? CategoryId { get; set; }

        public Country? Country { get; set; }
        public Guid? CountryId { get; set; }

        public Language? Language { get; set; }
        public Guid? LanguageId { get; set; }

    }
}
