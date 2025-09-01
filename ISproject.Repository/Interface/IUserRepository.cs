using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;

namespace ISproject.Repository.Interface
{
    public interface IUserRepository
    {
        NewsUser FindUserById(string id);
        NewsUser FindUserByIdNoData(string id);
    }
}
