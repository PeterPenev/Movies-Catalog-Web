using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using MoviesCatalog.Web.Utils;

namespace MoviesCatalog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly IGenreService genreService;
        private readonly IMovieService movieService;
        private readonly IViewModelMapper<Genre, GenreViewModel> genreViewMapper;

        public GenresController(IGenreService genreService,
                                IMovieService movieService,
                                IViewModelMapper<Genre, GenreViewModel> genreViewMapper)
        {
            this.genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.genreViewMapper = genreViewMapper ?? throw new ArgumentNullException(nameof(genreViewMapper));
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
                    StatusMessage = string.Format(WebConstants.GenreAlreadyExists, model.Name);
                    
                    return RedirectToAction("Create", "Genres");
                }

                var genre = await this.genreService
                        .CreateGenreAsync(model.Name);

                StatusMessage = string.Format(WebConstants.GenreSuccessfullyCreated, model.Name);                

                return RedirectToAction("Create", "Genres");
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddGenreToMovie(int id)
        {
            var genres = await this.genreService.GetAllGenresAsync();

            if (genres == null)
            {
                return NotFound();
            }

            var genreViewModel = genres.Select(this.genreViewMapper.MapFrom).ToList();

            ViewData["MovieId"] = id;
            return View(genreViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGenreToMovie(int genreId, int movieId)
        {
            try
            {
                var movie = await this.movieService.GetMovieByIdAsync(movieId);
                if (movie == null)
                {
                    return NotFound();
                }
                var genre = await this.genreService.GetGenreByIdAsync(genreId);
                if (genre == null)
                {
                    return NotFound();
                }

                await this.genreService.AddGenreToMovieAsync(movie.Id, genre.Id);

                StatusMessage = string.Format(WebConstants.SuccessfullyAddGenreToMovie, genre.Name, movie.Title);
                
                return RedirectToAction("Details", "Movies", new { id = movie.Id });
            }
            catch (ArgumentException ex)
            {
                StatusMessage = ex.Message;

                return RedirectToAction("AddGenreToMovie", "Genres");
            }
        }
    }
}