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
using MoviesCatalog.Web.Services.Contracts;

namespace MoviesCatalog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieViewMapper;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IImageOptimizer optimizer;

        public MoviesController(IMovieService movieService,
                                IViewModelMapper<Movie, MovieViewModel> movieViewMapper,
                                IImageOptimizer optimizer,
                                SignInManager<ApplicationUser> signInManager)
        {
            this.optimizer = optimizer;
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
                if (await this.movieService.IsMovieExistAsync(model.Title))
                {
                    StatusMessage = $"Movie with title \"{model.Title}\" already exists.";

                    return RedirectToAction("Create", "Movies");

                }

                StatusMessage = $"Successfully added movie with title \"{model.Title}\".";

                string posterName = null;
                string sliderName = null;

                if (model.PosterImage != null)
                {
                    posterName = optimizer.OptimizeImage(model.PosterImage, 268, 182);
                }

                if (model.SliderPoster != null)
                {
                    sliderName = optimizer.OptimizeImage(model.SliderPoster, 500, 1300);
                }

                var movie = await this.movieService
                        .CreateMovieAsync(model.Title, model.Trailer, posterName, sliderName, model.Description, model.ReleaseDate, model.UserId);

                return RedirectToAction("Details", "Movies", new { id = movie.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var movie = await this.movieService.GetMovieByIdAsync(id);
            var movieViewModel = this.movieViewMapper.MapFrom(movie);
            return View(movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MovieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var movie = await this.movieService.GetMovieByIdAsync(model.Id);
                if (movie == null)
                {
                    return NotFound();
                }

                string posterName = null;
                string sliderName = null;

                if (model.PosterImage != null)
                {
                    posterName = optimizer.OptimizeImage(model.PosterImage, 268, 182);
                }

                if (model.SliderPoster != null)
                {
                    sliderName = optimizer.OptimizeImage(model.SliderPoster, 500, 1300);
                }
                if (model.Poster != null)
                {
                    optimizer.DeleteOldImage(model.Poster);
                }
                if (model.SliderImage != null)
                {
                    optimizer.DeleteOldImage(model.SliderImage);
                }
               
                movie = await this.movieService
                                  .UpdateMovieAsync(movie, model.Description, posterName, sliderName);

                if (movie.Description == model.Description && movie.Poster == posterName && movie.SliderImage == sliderName)
                {
                    StatusMessage = $"Successfully updated details of movie with title \"{model.Title}\"";
                }
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