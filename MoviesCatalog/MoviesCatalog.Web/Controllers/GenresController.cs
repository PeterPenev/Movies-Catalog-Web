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
    public class GenresController : Controller
    {
        private readonly IGenreService genreService;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieViewMapper;

        public GenresController(IGenreService genreService,
                                IViewModelMapper<Movie, MovieViewModel> movieViewMapper)
        {
            this.genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            this.movieViewMapper = movieViewMapper ?? throw new ArgumentNullException(nameof(movieViewMapper));

        }


        //public IActionResult Details(string id)
        //{
        //    var movie = this.movieService.GetMovieById(id);

        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(this.movieViewMapper.MapFrom(movie));
        //}

        public IActionResult MoviesByGenre(string id)
        {
            var moviesByGenre = this.genreService.ShowMoviesByGenre(id);

            var movieIndexView = new MovieIndexViewModel()
            {
                MoviesByGenre = moviesByGenre.Select(this.movieViewMapper.MapFrom).ToList()
            };

            var allGenresWithCountMovies = genreService.GetAllGenresWithCountOfMovies();
            ViewBag.AllGenresWithCountOfMovies = allGenresWithCountMovies;

            return View(movieIndexView);              
        }

        public IActionResult Index()
        {
            var allGenres = genreService.GetAllGenres();

            var countOfGenres = allGenres.Count();

            var allGenresWithCountMovies = genreService.GetAllGenresWithCountOfMovies();                       

            ViewBag.AllGenres = allGenres;
            ViewBag.CountOfGenres = countOfGenres;
            ViewBag.AllGenresWithCountOfMovies = allGenresWithCountMovies;

            return View();
        }
    }
}