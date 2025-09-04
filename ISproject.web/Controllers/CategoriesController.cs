using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoriesController : Controller
    {
       private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }



        // GET: Categories
        public IActionResult Index()
        {
            if(categoryService.GetAll().Count() == 0)
            {
                FillData();
            }
            return View(categoryService.GetAll());
        }

        // GET: Categories/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryService.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Code,Description,Id")] Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryService.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, [Bind("Code,Description,Id")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                categoryService.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryService.GetById(id.Value);    
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }



        private void FillData()
        {
            List<Category> catData = [ 
                new Category { Code = "general", Description = "Uncategorized News" },
                new Category { Code = "business", Description = "Business News" },
                new Category { Code = "entertainment", Description = "Entertainment News" },
                new Category { Code = "health", Description = "Health News" },
                new Category { Code = "science", Description = "Science News" },
                new Category { Code = "sports", Description = "Sports News" },
                new Category { Code = "technology", Description = "Technology News" },
             ];

            catData.ForEach(c => { categoryService.Add(c); });
        }
    }
}
