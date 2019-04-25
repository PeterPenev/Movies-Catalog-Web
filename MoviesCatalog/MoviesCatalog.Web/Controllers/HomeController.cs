using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieMapper;

        public HomeController(IMovieService movieService,
                              
                              IViewModelMapper<Movie, MovieViewModel> movieMapper)
        {
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.movieMapper = movieMapper ?? throw new ArgumentNullException(nameof(movieMapper));
        }

        public IActionResult Index()
        {
            var topMoviesByRating = movieService.ShowMoviesTop10ByRaiting();
            var topMoviesByReleaseDate = movieService.ShowMoviesLatest10ByReleaseDate();
            //var collections = new List<IReadOnlyCollection<Movie>>() { topMoviesByRating, topMoviesByReleaseDate };
            //var homeViewModel = this.homeMapper.MapFrom(collections);
            var homeViewModel = new HomeViewModel()
            {
                TopTenMoviesByRating = topMoviesByRating.Select(this.movieMapper.MapFrom).ToList(),
                TopTenMoviesByReleaseDate = topMoviesByReleaseDate.Select(this.movieMapper.MapFrom).ToList()
            };
           
            return View(homeViewModel);
        }

        public IActionResult Genres()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
