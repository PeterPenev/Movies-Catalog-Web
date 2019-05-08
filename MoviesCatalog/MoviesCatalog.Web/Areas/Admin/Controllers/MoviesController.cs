using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Migrations;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieViewMapper;
        private readonly SignInManager<ApplicationUser> signInManager;

        public MoviesController(IMovieService movieService,
                                IViewModelMapper<Movie, MovieViewModel> movieViewMapper,
                                SignInManager<ApplicationUser> signInManager)
        {
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.movieViewMapper = movieViewMapper ?? throw new ArgumentNullException(nameof(movieViewMapper));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["UserId"] = signInManager.UserManager.Users.First(u => u.UserName == User.Identity.Name).Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await this.movieService.IsMovieExist(model.Title))
                {
                    StatusMessage = $"Movie with title \"{model.Title}\" already exists.";

                    return RedirectToAction("Create", "Movies");

                }

                StatusMessage = $"Successfully added movie with title \"{model.Title}\".";



                var movie = await this.movieService
                        .CreateMovie(model.Title, model.Trailer, model.Poster, model.Description, model.ReleaseDate, model.UserId);
               
                return RedirectToAction("Details", "Movies", new { id = movie.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }
    }
}