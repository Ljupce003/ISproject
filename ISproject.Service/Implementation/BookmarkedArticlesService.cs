using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISproject.Domain.Models;
using ISproject.Repository.Implementation;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Service.Implementation
{
    public class BookmarkedArticlesService : IBookmarkedArticlesService
    {
        private readonly IRepository<BookmarkedArticle> repository;
        private readonly IUserRepository userRepository;

        private readonly IBookmarkCartService bookmarkCartService;

        public BookmarkedArticlesService(IRepository<BookmarkedArticle> repository, IUserRepository userRepository, IBookmarkCartService bookmarkCartService)
        {
            this.repository = repository;
            this.userRepository = userRepository;
            this.bookmarkCartService = bookmarkCartService;
        }

        public BookmarkedArticle Add(BookmarkedArticle article)
        {
            return repository.Insert(article);
        }

        public IEnumerable<BookmarkedArticle> AddAll(IEnumerable<BookmarkedArticle> articles)
        {
            return repository.InsertMany(articles);
        }

        public BookmarkedArticle Delete(Guid id)
        {
            var article = GetById(id);
            if (article == null)
            {
                throw new ArgumentException("BookmarkedArticle not found", nameof(id));
            }

            return repository.Delete(article);
        }

        public IEnumerable<BookmarkedArticle> GetAll()
        {
            return repository.GetAll(selector: a => a, include: ai => ai
            .Include(aa => aa.Article!)
            .Include(ab => ab.BookmarkCart!));
        }

        public IEnumerable<BookmarkedArticle> GetAllbyUser(string? userId)
        {
            if (userId == null)
            {
                throw new Exception("UserId is null");
            }

            var user = userRepository.FindUserById(userId);
            if (user == null)
            {
                throw new Exception("User is Not Found");
            }

            var cart = bookmarkCartService.GetByUserId(userId);


            return repository.GetAll(
                    selector: a => a,
                    predicate: p => p.BookmarkCart == cart,
                    include: ai => ai
                     .Include(aa => aa.Article!)
                     .Include(ab => ab.BookmarkCart!));



        }

        public BookmarkedArticle? GetById(Guid Id)
        {
            return repository.Get(selector: a => a, predicate: p => p.Id == Id, include: ai => ai
            .Include(aa => aa.Article!)
            .Include(ab => ab.BookmarkCart!));
        }

        public BookmarkedArticle Update(BookmarkedArticle article)
        {
            return repository.Update(article);
        }
    }
}
