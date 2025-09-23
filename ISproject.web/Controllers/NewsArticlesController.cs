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
    public class NewsArticlesController : Controller
    {
        private readonly INewsArticleService newsArticleService;
        private readonly ICategoryService categoryService;
        private readonly ICountryService countryService;
        private readonly ILanguageService languageService;
        private readonly INewsSourceService newsSourceService;

        public NewsArticlesController(INewsArticleService newsArticleService,
            ICategoryService categoryService, 
            ICountryService countryService,
            ILanguageService languageService, 
            INewsSourceService newsSourceService)
        {
            this.newsArticleService = newsArticleService;
            this.categoryService = categoryService;
            this.countryService = countryService;
            this.languageService = languageService;
            this.newsSourceService = newsSourceService;
        }




        // GET: NewsArticles
        public IActionResult Index()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll().OrderBy(cat => cat.Code), "Code", "Code");
            ViewData["CountryId"] = new SelectList(countryService.GetAll().OrderBy(country => country.Name), "Code", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll().OrderBy(lang => lang.Name), "Code", "Name");
            return View(newsArticleService.GetAll().OrderByDescending(na => na.PublishedAt));
        }

        // GET: NewsArticles/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = newsArticleService.GetById(id.Value);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // GET: NewsArticles/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Code");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll(), "Id", "Name");
            //ViewData["SourceId"] = new SelectList(newsSourceService.GetAll(), "Id", "Code");
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Author,Title,Description,Url,Source,ImageUrl,CategoryId,LanguageId,CountryId,PublishedAt,Id")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                newsArticleService.Add(newsArticle);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Code");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll(), "Id", "Name");
            //ViewData["SourceId"] = new SelectList(newsSourceService.GetAll(), "Id", "Code");
            return View(newsArticle);
        }

        // GET: NewsArticles/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = newsArticleService.GetById(id.Value);
            if (newsArticle == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Code");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll(), "Id", "Name");
            //ViewData["SourceId"] = new SelectList(newsSourceService.GetAll(), "Id", "Code");
            return View(newsArticle);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, [Bind("Author,Title,Description,Url,Source,ImageUrl,CategoryId,LanguageId,CountryId,PublishedAt,Id")] NewsArticle newsArticle)
        {
            if (id != newsArticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                newsArticleService.Update(newsArticle);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Code");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll(), "Id", "Name");
            //ViewData["SourceId"] = new SelectList(newsSourceService.GetAll(), "Id", "Code");
            return View(newsArticle);
        }

        // GET: NewsArticles/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = newsArticleService.GetById(id.Value);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            newsArticleService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult BookMarkArticles(List<string> articleId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookMarked = newsArticleService.BookmarkArticles(userId, articleId);

            return RedirectToAction("Index", "BookmarkedArticles");
        }

   
    }
}
