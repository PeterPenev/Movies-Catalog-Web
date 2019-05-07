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

        public async Task<IActionResult> Index()
        {
            var topMoviesByRating = await this.movieService.ShowMoviesTop10ByRaiting();
            var lastMoviesByReleaseDate = await this.movieService.ShowMoviesLatest6ByReleaseDate();
            
            var homeViewModel = new HomeViewModel()
            {
                TopTenMoviesByRating = topMoviesByRating.Select(this.movieMapper.MapFrom).ToList(),
                TopTenMoviesByReleaseDate = lastMoviesByReleaseDate.Select(this.movieMapper.MapFrom).ToList()
            };
           
            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
