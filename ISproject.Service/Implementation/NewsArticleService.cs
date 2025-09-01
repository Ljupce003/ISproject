using System.Security.Cryptography;
using System.Text;
using ISproject.Domain.Models;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ISproject.Service.Implementation
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly IRepository<NewsArticle> _repository;
        private readonly IUserRepository userRepository;
        private readonly IBookmarkCartService bookmarkCartService;
        private readonly IBookmarkedArticlesService bookmarkedArticlesService;

        public NewsArticleService(
            IRepository<NewsArticle> repository,
            IUserRepository userRepository,
            IBookmarkCartService bookmarkCartService,
            IBookmarkedArticlesService bookmarkedArticlesService)
        {
            _repository = repository;
            this.userRepository = userRepository;
            this.bookmarkCartService = bookmarkCartService;
            this.bookmarkedArticlesService = bookmarkedArticlesService;
        }

        public NewsArticle? Add(NewsArticle newsArticle)
        {
            newsArticle.Code = GenerateIdentifier(newsArticle);
            if (GetByCode(newsArticle.Code) == null) return _repository.Insert(newsArticle);
            else throw new Exception("Article already exists");
        }

        public NewsArticle Delete(Guid id)
        {
            var newsArticle = GetById(id);
            if (newsArticle == null)
            {
                throw new ArgumentException("NewsArticle not found", nameof(id));
            }

            return _repository.Delete(newsArticle);
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return _repository.GetAll(selector: c => c,
                include: ni => ni
                    .Include(n => n.Category!)
                    .Include(n => n.Country!)
                    .Include(n => n.Language!));
        }

        public NewsArticle? GetById(Guid id)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Id == id, include: ni => ni
                    .Include(n => n.Category!)
                    .Include(n => n.Country!)
                    .Include(n => n.Language!));
        }

        public NewsArticle Update(NewsArticle newsArticle)
        {
            return _repository.Update(newsArticle);
        }



        public static string GenerateIdentifier(NewsArticle newsArticle)
        {
            string raw = $"{newsArticle.Title}-{newsArticle.PublishedAt:yyyy-MM-ddTHH:mm}";
            var sha = SHA256.Create();
            var hashbytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
            return Convert.ToBase64String(hashbytes);
        }

        public NewsArticle? GetByCode(string code)
        {
            return _repository.Get(selector: c => c, predicate: p => p.Code == code, include: ni => ni
                    .Include(n => n.Category!)
                    .Include(n => n.Country!)
                    .Include(n => n.Language!));
        }

        public IEnumerable<NewsArticle?> AddAll(IEnumerable<NewsArticle> newsArticles)
        {
            var newArticles = newsArticles
                .Select(article =>
                {
                    article.Code = GenerateIdentifier(article);
                    return article;
                })
                .ToList(); // force evaluation

            foreach (var article in newArticles)
            {
                if(GetByCode(article.Code!) == null) _repository.Insert(article); // insert each one
            }

            return newArticles;
        }

        public IEnumerable<BookmarkedArticle?> BookmarkArticles(string? userId, List<string> articleIds)
        {
            if(userId == null)
            {
                throw new Exception("UserId is null");
            }

            var user = userRepository.FindUserById(userId);
            var cart = user.BookmarkCart;
            var existingBookmarkedArticleIds = bookmarkedArticlesService.GetAll()
                    .Where(ba => ba.BookmarkCart.UserId == userId)
                    .Select(ba => ba.ArticleId)
                    .ToHashSet();

            if (cart == null) {
                cart = new BookmarkCart { User = user};
                cart = bookmarkCartService.Add(cart);
                
            }

            List<BookmarkedArticle> bookmarkedArticles = [];

            articleIds.ForEach(articleId =>
            {
                var article = GetById(Guid.Parse(articleId));

                if (article != null)
                {
                    
                    if (!existingBookmarkedArticleIds.Contains(Guid.Parse(articleId)))
                    {
                        bookmarkedArticles.Add(
                                                new BookmarkedArticle
                                                {
                                                    Article = article,
                                                    BookmarkCart = cart,
                                                    AddedAt = DateTime.Now
                                                });

                    }

                }
            });

            var savedBookmarks = bookmarkedArticlesService.AddAll(bookmarkedArticles);

            return savedBookmarks;
        }
    }
}
