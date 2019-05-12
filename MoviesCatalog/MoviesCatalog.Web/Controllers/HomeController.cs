using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
            var topMoviesByRating = await this.movieService.ShowMoviesTop10ByRaitingAsync();
            var topMoviesByRatingWithSlider = await this.movieService.ShowMoviesTop10ByRaitingContainsSliderImageAsync();
            var lastMoviesByReleaseDate = await this.movieService.ShowMoviesLatest6ByReleaseDateAsync();
            
            var homeViewModel = new HomeViewModel()
            {
                TopTenMoviesByRating = topMoviesByRating.Select(this.movieMapper.MapFrom).ToList(),
                TopTenMoviesByRatingWithSlider = topMoviesByRatingWithSlider.Select(this.movieMapper.MapFrom).ToList(),
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
