using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models.dto;

namespace ISproject.Domain.Models.Responses
{
    public class ApiErrorResponse
    {
        public ApiErrorDTO? Error { get; set; }
    }
}
