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
        private readonly IViewModelMapper<Review,ReviewViewModel> reviewViewMapper;

        public MoviesController(IMovieService movieService,
                                IViewModelMapper<Movie, MovieViewModel> movieViewMapper,
                                IViewModelMapper<Review, ReviewViewModel> reviewViewMapper)
        {
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.movieViewMapper = movieViewMapper ?? throw new ArgumentNullException(nameof(movieViewMapper));
            this.reviewViewMapper = reviewViewMapper ?? throw new ArgumentNullException(nameof(reviewViewMapper));
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
        public IActionResult Search(SearchMovieViewModel model)
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
        
        public async Task<IActionResult> MoviesByName(char id)
        {
            var moviesByStartingSymbol = await this.movieService.ShowMoviesStartWithSymbol(id);

            var movieViewModel = moviesByStartingSymbol.Select(this.movieViewMapper.MapFrom).ToList();            
            
            return View(movieViewModel);
        }

        public async Task<IActionResult> Index()
        {
            var movies = await this.movieService.ShowAllMoviesOrderedDescByRating();
            var movieViewModel = movies.Select(this.movieViewMapper.MapFrom).ToList();
            return View(movieViewModel);
        }        

        public async Task<IActionResult> AllReviewsByMovie (int movieId)
        {
            var allReviewsByMovie = await this.movieService.AllReviewsByMovie(movieId);

            var reviewIndexView = new ReviewViewModel()
            {
                 AllReviewsByMovie= allReviewsByMovie.Select(this.reviewViewMapper.MapFrom).ToList()
            };

            return View(reviewIndexView);
        }

        public async Task<IActionResult> LastFiveReviewsByMovie(int movieId)
        {
            var lastFiveReviewsByMovie = await this.movieService.LastFiveReviewsByMovie(movieId);

            var reviewIndexView = new ReviewViewModel()
            {
                LastFiveReviewsByMovie = lastFiveReviewsByMovie.Select(this.reviewViewMapper.MapFrom).ToList()
            };

            return View(reviewIndexView);
        }      
    }
}