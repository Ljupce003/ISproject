using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ISproject.Domain.Models;
using ISproject.Repository.Data;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ISproject.web.Controllers
{
    [Authorize]
    public class ArticleInFoldersController : Controller
    {
        private readonly IArticleInFolderService articleInFolderService;
        private readonly IBookMarkFolderService bookMarkFolderService;
        private readonly INewsArticleService newsArticleService;

        public ArticleInFoldersController(IArticleInFolderService articleInFolderService, IBookMarkFolderService bookMarkFolderService, INewsArticleService newsArticleService)
        {
            this.articleInFolderService = articleInFolderService;
            this.bookMarkFolderService = bookMarkFolderService;
            this.newsArticleService = newsArticleService;
        }




        // GET: ArticleInFolders
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(articleInFolderService.GetAll().Where(af => af.BookmarkFolder!.UserId == userId));
        }

        // GET: ArticleInFolders/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleInFolder = articleInFolderService.GetById(id.Value);
            if (articleInFolder == null)
            {
                return NotFound();
            }

            return View(articleInFolder);
        }

        // GET: ArticleInFolders/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["BookmarkFolderId"] = new SelectList(bookMarkFolderService.GetFolderNamesByUserId(userId), "Id", "Name");
            ViewData["NewsArticleId"] = new SelectList(newsArticleService.GetAll(), "Id", "Title");
            return View();
        }

        // POST: ArticleInFolders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NewsArticleId,BookmarkFolderId,DateAdded,Id")] ArticleInFolder articleInFolder)
        {
            if (ModelState.IsValid)
            {
                articleInFolderService.Add(articleInFolder);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "BookmarkFolders");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["BookmarkFolderId"] = new SelectList(bookMarkFolderService.GetFolderNamesByUserId(userId), "Id", "Name");
            ViewData["NewsArticleId"] = new SelectList(newsArticleService.GetAll(), "Id", "Title");
            return View(articleInFolder);
        }

        // GET: ArticleInFolders/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleInFolder = articleInFolderService.GetById(id.Value);
            if (articleInFolder == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["BookmarkFolderId"] = new SelectList(bookMarkFolderService.GetFolderNamesByUserId(userId), "Id", "Name");
            ViewData["NewsArticleId"] = new SelectList(newsArticleService.GetAll(), "Id", "Title");
            return View(articleInFolder);
        }

        // POST: ArticleInFolders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("NewsArticleId,BookmarkFolderId,DateAdded,Id")] ArticleInFolder articleInFolder)
        {
            if (id != articleInFolder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                articleInFolderService.Update(articleInFolder);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "BookmarkFolders");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["BookmarkFolderId"] = new SelectList(bookMarkFolderService.GetFolderNamesByUserId(userId), "Id", "Name");
            ViewData["NewsArticleId"] = new SelectList(newsArticleService.GetAll(), "Id", "Title");
            return View(articleInFolder);
        }

        // GET: ArticleInFolders/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleInFolder = articleInFolderService.GetById(id.Value);
            if (articleInFolder == null)
            {
                return NotFound();
            }

            return View(articleInFolder);
        }

        // POST: ArticleInFolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            articleInFolderService.Delete(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "BookmarkFolders");
        }


    }
}
