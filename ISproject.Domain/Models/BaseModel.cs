using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Domain.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
