using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISproject.Domain.Models.dto
{
    public class ApiErrorDTO
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public Dictionary<string,List<string>>? Context { get; set; }
    }
}
