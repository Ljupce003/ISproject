using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ISproject.Domain.Models;
using ISproject.Repository.Data;
using ISproject.Repository.Interface;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ISproject.web.Controllers
{
    [Authorize]
    public class BookmarkFoldersController : Controller
    {
        private readonly IBookMarkFolderService bookMarkFolderService;
        private readonly IArticleInFolderService articleInFolderService;
        private readonly IUserRepository userRepository;

        public BookmarkFoldersController(IBookMarkFolderService bookMarkFolderService, IArticleInFolderService articleInFolderService, IUserRepository userRepository)
        {
            this.bookMarkFolderService = bookMarkFolderService;
            this.articleInFolderService = articleInFolderService;
            this.userRepository = userRepository;
        }




        // GET: BookmarkFolders

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(bookMarkFolderService.GetFoldersByUserId(userId));
        }

        // GET: BookmarkFolders/Details/5
        
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarkFolder = bookMarkFolderService.GetById(id.Value);
            if (bookmarkFolder == null)
            {
                return NotFound();
            }

            return View(bookmarkFolder);
        }

        // GET: BookmarkFolders/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            return View();
        }

        // POST: BookmarkFolders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,UserId,Id")] BookmarkFolder bookmarkFolder)
        {
            if (ModelState.IsValid)
            {
                bookMarkFolderService.Add(bookmarkFolder);
                return RedirectToAction(nameof(Index));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            return View(bookmarkFolder);
        }

        // GET: BookmarkFolders/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarkFolder = bookMarkFolderService.GetById(id.Value);
            if (bookmarkFolder == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            return View(bookmarkFolder);
        }

        // POST: BookmarkFolders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,UserId,Id")] BookmarkFolder bookmarkFolder)
        {
            if (id != bookmarkFolder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bookMarkFolderService.Update(bookmarkFolder);
                return RedirectToAction(nameof(Index));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            return View(bookmarkFolder);
        }

        // GET: BookmarkFolders/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmarkFolder = bookMarkFolderService.GetById(id.Value);
            if (bookmarkFolder == null)
            {
                return NotFound();
            }

            return View(bookmarkFolder);
        }

        // POST: BookmarkFolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            bookMarkFolderService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("DeleteWholeFolder")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWholeConfirmed(Guid id)
        {
            articleInFolderService.DeleteGrouped(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult ExportToCSV(Guid id)
        {
            var data = bookMarkFolderService.ExportFileToCSV(id);

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = bookMarkFolderService.GetByIdNoData(id)!.Name;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return BadRequest("User Id is not found");

            var user = userRepository.FindUserByIdNoData(userId);

            if (user == null) return BadRequest("User is not found");

            return File(data, contentType, $"{fileName} - {user.UserName}.xlsx");

        }





    }
}
