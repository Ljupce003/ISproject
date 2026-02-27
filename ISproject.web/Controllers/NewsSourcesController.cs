using ISproject.Domain.Models;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISproject.web.Controllers
{
    public class NewsSourcesController : Controller
    {
        private readonly INewsSourceService newsSourceService;
        private readonly ICategoryService categoryService;
        private readonly ICountryService countryService;
        private readonly ILanguageService languageService;

        public NewsSourcesController(
            INewsSourceService newsSourceService,
            ICategoryService categoryService,
            ICountryService countryService,
            ILanguageService languageService)
        {
            this.newsSourceService = newsSourceService;
            this.categoryService = categoryService;
            this.countryService = countryService;
            this.languageService = languageService;
        }

        // GET: NewsSources
        public IActionResult Index()
        {

            return View(newsSourceService.GetAll());
        }

        // GET: NewsSources/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsSource = newsSourceService.GetById(id.Value);
            if (newsSource == null)
            {
                return NotFound();
            }

            return View(newsSource);
        }

        // GET: NewsSources/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Description");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: NewsSources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Code,Name,Url,CategoryId,CountryId,LanguageId,Id")] NewsSource newsSource)
        {
            if (ModelState.IsValid)
            {
                newsSourceService.Add(newsSource);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Description");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll(), "Id", "Name");
            return View(newsSource);
        }

        // GET: NewsSources/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsSource = newsSourceService.GetById(id.Value);
            if (newsSource == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Description");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll(), "Id", "Name");
            return View(newsSource);
        }

        // POST: NewsSources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Code,Name,Url,CategoryId,CountryId,LanguageId,Id")] NewsSource newsSource)
        {
            if (id != newsSource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                newsSourceService.Update(newsSource);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Description");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["LanguageId"] = new SelectList(languageService.GetAll(), "Id", "Name");
            return View(newsSource);
        }

        // GET: NewsSources/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsSource = newsSourceService.GetById(id.Value);
            if (newsSource == null)
            {
                return NotFound();
            }

            return View(newsSource);
        }

        // POST: NewsSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            newsSourceService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
