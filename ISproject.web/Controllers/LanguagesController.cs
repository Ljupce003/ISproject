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

namespace ISproject.web.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly ILanguageService languageService;

        public LanguagesController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }



        // GET: Languages
        public IActionResult Index()
        {
            if(languageService.GetAll().Count() == 0)
            {
                FillData();
            }
            return View(languageService.GetAll());
        }

        // GET: Languages/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = languageService.GetById(id.Value);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // GET: Languages/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Name,Code,Id")] Language language)
        {
            if (ModelState.IsValid)
            {
                languageService.Add(language);
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Languages/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = languageService.GetById(id.Value);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, [Bind("Name,Code,Id")] Language language)
        {
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                languageService.Update(language);
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Languages/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = languageService.GetById(id.Value);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            languageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public IActionResult FillMissingData()
        {
            FillData();

            return RedirectToAction(nameof(Index));
        }

        private void FillData(){

            List<Language> languageData = GetData();

            languageData.ForEach(language =>
            {
                if(languageService.GetByCode(language.Code!) == null)languageService.Add(language);
            });

        }

        private static List<Language> GetData()
        {
            return [
                new Language { Id = Guid.NewGuid(), Code = "ar", Name = "Arabic"},
                new Language { Id = Guid.NewGuid(), Code = "de", Name = "German"},
                new Language { Id = Guid.NewGuid(), Code = "en", Name = "English"},
                new Language { Id = Guid.NewGuid(), Code = "es", Name = "Spanish"},
                new Language { Id = Guid.NewGuid(), Code = "fr", Name = "French"},
                new Language { Id = Guid.NewGuid(), Code = "he", Name = "Hebrew"},
                new Language { Id = Guid.NewGuid(), Code = "it", Name = "Italian"},
                new Language { Id = Guid.NewGuid(), Code = "nl", Name = "Dutch"},
                new Language { Id = Guid.NewGuid(), Code = "no", Name = "Norwegian"},
                new Language { Id = Guid.NewGuid(), Code = "pt", Name = "Portuguese"},
                new Language { Id = Guid.NewGuid(), Code = "ru", Name = "Russian"},
                new Language { Id = Guid.NewGuid(), Code = "se", Name = "Swedish"},
                new Language { Id = Guid.NewGuid(), Code = "sv", Name = "Swedish"},
                new Language { Id = Guid.NewGuid(), Code = "zh", Name = "Chinese"}
                ];
        }
    }
}
