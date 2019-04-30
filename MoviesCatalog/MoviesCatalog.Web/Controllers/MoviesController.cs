using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieViewMapper;

        public MoviesController(IMovieService movieService,
                                IViewModelMapper<Movie, MovieViewModel> movieViewMapper)
        {
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.movieViewMapper = movieViewMapper ?? throw new ArgumentNullException(nameof(movieViewMapper));

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var movie = this.movieService
                                .CreateMovie(model.Title, model.Trailer, model.Poster, model.Description, model.ReleaseDate);


                return RedirectToAction(nameof(Index), new { id = movie.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Search([FromQuery]SearchMovieViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SearchName) ||
                model.SearchName.Length < 3)
            {
                return View();
            }

            model.SearchResults = this.movieService.SearchMoviesContainsString(model.SearchName)
                                                    .Select(this.movieViewMapper.MapFrom)
                                                    .ToList();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var movie = this.movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(this.movieViewMapper.MapFrom(movie));
        }        

        public IActionResult MoviesByName(int id)
        {
            //char symbol = (char)id;

            var moviesByStartingSymbol = this.movieService.ShowMoviesStartWithSymbol(id);

            var movieIndexView = new MovieIndexViewModel()
            {
                MoviesByName = moviesByStartingSymbol.Select(this.movieViewMapper.MapFrom).ToList()
            };

            return View(movieIndexView);
        }

        public IActionResult Index()
        {
            var showLatest10MoviesByReleaseDate = movieService.ShowMoviesLatest10ByReleaseDate();

            var movieIndexView = new MovieIndexViewModel()
            {
                Latest10Movies = showLatest10MoviesByReleaseDate.Select(this.movieViewMapper.MapFrom).ToList()
            };
            return View(movieIndexView);
        }
    }
}