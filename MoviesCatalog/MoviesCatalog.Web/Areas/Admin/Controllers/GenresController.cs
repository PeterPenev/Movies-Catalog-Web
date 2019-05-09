using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await this.genreService.IsGenreExistAsync(model.Name))
                {
                    StatusMessage = $"Genre with name \"{model.Name}\" already exists.";

                    return RedirectToAction("Create", "Genres");

                }               

                var genre = await this.genreService
                        .CreateGenreAsync(model.Name);

                StatusMessage = $"Successfully added genre with name \"{model.Name}\".";

                return RedirectToAction("Create", "Genres");
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }
    }
}