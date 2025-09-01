using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISproject.Domain.Models;
using ISproject.Repository.Data;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ISproject.web.Controllers
{
    public class BookmarkedArticlesController : Controller
    {
        private readonly IBookmarkedArticlesService bookmarkedArticlesService;
        private readonly IBookmarkCartService bookmarkCartService;
        private readonly INewsArticleService newsArticleService;
        private readonly IBookMarkFolderService bookMarkFolderService;
        private readonly IArticleInFolderService articleInFolderService;


        public BookmarkedArticlesController(IBookmarkedArticlesService bookmarkedArticlesService, IBookmarkCartService bookmarkCartService, INewsArticleService newsArticleService, IBookMarkFolderService bookMarkFolderService, IArticleInFolderService articleInFolderService)
        {
            this.bookmarkedArticlesService = bookmarkedArticlesService;
            this.bookmarkCartService = bookmarkCartService;
            this.newsArticleService = newsArticleService;
            this.bookMarkFolderService = bookMarkFolderService;
            this.articleInFolderService = articleInFolderService;
        }




        // GET: BookmarkedArticles
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["Folders"] = new SelectList(bookMarkFolderService.GetFolderNamesByUserId(userId), "Id", "Name");
            return View(bookmarkedArticlesService.GetAllbyUser(userId));
        }

        // GET: BookmarkedArticles/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarkedArticle = bookmarkedArticlesService.GetById(id.Value);
            if (bookmarkedArticle == null)
            {
                return NotFound();
            }

            return View(bookmarkedArticle);
        }

        [Authorize]
        // GET: BookmarkedArticles/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = bookmarkCartService.GetByUserId(userId);

            ViewData["ArticleId"] = new SelectList(newsArticleService.GetAll(), "Id", "Title");
            ViewData["BookmarkCartId"] = cart.Id;
            return View();
        }

        // POST: BookmarkedArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ArticleId,BookmarkCartId,AddedAt,Id")] BookmarkedArticle bookmarkedArticle)
        {
            if (ModelState.IsValid)
            {
                bookmarkedArticlesService.Add(bookmarkedArticle);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(newsArticleService.GetAll(), "Id", "Title");
            ViewData["BookmarkCartId"] = bookmarkedArticle.BookmarkCartId;
            return View(bookmarkedArticle);
        }

        // GET: BookmarkedArticles/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarkedArticle = bookmarkedArticlesService.GetById(id.Value);
            if (bookmarkedArticle == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(newsArticleService.GetAll(), "Id", "Title");
            ViewData["BookmarkCartId"] = bookmarkedArticle.BookmarkCartId;
            return View(bookmarkedArticle);
        }

        // POST: BookmarkedArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("ArticleId,BookmarkCartId,AddedAt,Id")] BookmarkedArticle bookmarkedArticle)
        {
            if (id != bookmarkedArticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bookmarkedArticlesService.Update(bookmarkedArticle);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(newsArticleService.GetAll(), "Id", "Title");
            ViewData["BookmarkCartId"] = bookmarkedArticle.BookmarkCartId;
            return View(bookmarkedArticle);
        }

        // GET: BookmarkedArticles/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarkedArticle = bookmarkedArticlesService.GetById(id.Value);
            if (bookmarkedArticle == null)
            {
                return NotFound();
            }

            return View(bookmarkedArticle);
        }

        // POST: BookmarkedArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            bookmarkedArticlesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddArticlesToFolder(string? folder, string? folderName, List<string> bookmarkId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var afs = articleInFolderService.AddToFolder(userId,folder, folderName, bookmarkId);

            return RedirectToAction("Index", "BookmarkFolders");
        }



    }
}
