using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Domain.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Country : BaseModel
    {
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }

    }
}
