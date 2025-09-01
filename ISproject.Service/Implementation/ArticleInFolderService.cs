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
    public class ArticleInFolderService : IArticleInFolderService
    {
        private readonly IRepository<ArticleInFolder> repository;
        private readonly IUserRepository userRepository;
        private readonly IBookMarkFolderService bookMarkFolderService;
        private readonly IBookmarkedArticlesService bookmarkedArticlesService;

        public ArticleInFolderService(IRepository<ArticleInFolder> repository, IBookMarkFolderService bookMarkFolderService, IUserRepository userRepository, IBookmarkedArticlesService bookmarkedArticlesService)
        {
            this.repository = repository;
            this.bookMarkFolderService = bookMarkFolderService;
            this.userRepository = userRepository;
            this.bookmarkedArticlesService = bookmarkedArticlesService;
        }

        public ArticleInFolder Add(ArticleInFolder articleInFolder)
        {
            return repository.Insert(articleInFolder);
        }

        public IEnumerable<ArticleInFolder> AddAll(IEnumerable<ArticleInFolder> articlesInFolder)
        {
            return repository.InsertMany(articlesInFolder);
        }

        public IEnumerable<ArticleInFolder> AddToFolder(string? userId, string? folderObject, string? folderName, List<string> bookmarkIds)
        {
            if (userId == null) throw new Exception("userId is null");
            if (folderObject == null) throw new Exception("folderObject is null");

            var user = userRepository.FindUserById(userId);


            BookmarkFolder? folder;
            if (folderObject == "NewFolder")
            {
                folder = bookMarkFolderService.Add(new BookmarkFolder
                {
                    Name = folderName,
                    User = user,
                    UserId = userId
                });
            }
            else
            {
                folder = bookMarkFolderService.GetById(Guid.Parse(folderObject));
                if (folder == null) throw new Exception("Folder to be found is null");
            }

            List<ArticleInFolder> articlesInFolder = [];

            foreach(var bookmarkId in bookmarkIds)
            {
                var bookmark = bookmarkedArticlesService.GetById(Guid.Parse(bookmarkId));
                if (bookmark == null) throw new Exception("Bookmark id is incorrect");

                articlesInFolder.Add(new ArticleInFolder
                {
                    BookmarkFolder = folder,
                    BookmarkFolderId = folder.Id,
                    NewsArticle = bookmark.Article,
                    NewsArticleId = bookmark.ArticleId,
                    DateAdded = DateTime.Now
                });

                bookmarkedArticlesService.Delete(bookmark.Id);

            }

            var savedArticlesInFolder = AddAll(articlesInFolder);


            return savedArticlesInFolder;
            
        }

        public ArticleInFolder Delete(Guid id)
        {
            var articleInFolder = GetById(id);
            if (articleInFolder == null)
            {
                throw new ArgumentException("Article In Folder not found", nameof(id));
            }

            return repository.Delete(articleInFolder);
        }

        public BookmarkFolder DeleteGrouped(Guid folderId)
        {
            var folder = bookMarkFolderService.GetById(folderId);
            if (folder == null)
            {
                throw new ArgumentException("Folder not found", nameof(folderId));
            }

            var folderArticles = folder.ArticleInFolders;

            if (folderArticles != null)
            {
                foreach (var ArticleInFolder in folderArticles)
                {
                    Delete(ArticleInFolder.Id);
                }
            }



            return bookMarkFolderService.Delete(folder.Id);
        }

        public IEnumerable<ArticleInFolder> GetAll()
        {
            return repository.GetAll(selector: af => af,
                include: ai => ai
                    .Include(an => an.NewsArticle!)
                    .Include(af => af.BookmarkFolder!));
        }

        public ArticleInFolder? GetById(Guid id)
        {
            return repository.Get(selector: af => af,
                predicate: ap => ap.Id == id,
                include: ai => ai
                    .Include(an => an.NewsArticle!)
                    .Include(af => af.BookmarkFolder!));
        }

        public ArticleInFolder Update(ArticleInFolder articleInFolder)
        {
            return repository.Update(articleInFolder);
        }
    }
}
