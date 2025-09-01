using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Service.Implementation
{
    public class BookmarkCartService : IBookmarkCartService
    {
        private readonly IRepository<BookmarkCart> repository;
        private readonly IUserRepository userRepository;

        public BookmarkCartService(IRepository<BookmarkCart> repository, IUserRepository userRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
        }

        public BookmarkCart Add(BookmarkCart bookmarkCart)
        {
            return repository.Insert(bookmarkCart);
        }

        public BookmarkCart? GetById(Guid id)
        {
            return repository.Get(selector: c => c, 
                predicate: p => p.Id == id,
                include: ci => ci
                .Include(u => u!.User!));
        }

        public BookmarkCart GetByUserId(string? userId)
        {
            if (userId == null)
            {
                throw new Exception("UserId is null");
            }

            var user = userRepository.FindUserById(userId);
            var cart = user.BookmarkCart;

            if (cart == null)
            {
                cart = new BookmarkCart { User = user };
                cart = Add(cart);
                return cart;
            }
            else
            {
                return cart;
            }
        }
    }
}
