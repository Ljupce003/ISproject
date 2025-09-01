using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;
using ISproject.Repository.Data;
using ISproject.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<NewsUser> entities;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<NewsUser>();
        }

        public NewsUser FindUserById(string id)
        {
            return entities.Include(ic => ic.BookmarkCart).Include(ib => ib.BookmarkFolders).First(u => u.Id==id);
        }

        public NewsUser FindUserByIdNoData(string id)
        {
            return entities.First(u => u.Id == id);
        }
    }
}
