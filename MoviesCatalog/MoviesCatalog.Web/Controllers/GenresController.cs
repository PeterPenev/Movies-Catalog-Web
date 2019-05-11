using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService genreService;
        private readonly IMemoryCache cache;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieViewMapper;

        public GenresController(IGenreService genreService,
                                IMemoryCache cache,
                                IViewModelMapper<Movie, MovieViewModel> movieViewMapper)
        {
            this.genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.movieViewMapper = movieViewMapper ?? throw new ArgumentNullException(nameof(movieViewMapper));

        }

        public async Task<IActionResult> MoviesByGenre(string id)
        {
            var moviesByGenre = await this.genreService.ShowMoviesByGenreAsync(id);

            var movieModelView = moviesByGenre.Select(this.movieViewMapper.MapFrom).ToList();           
            
            var allGenresWithCountMovies = await genreService.GetAllGenresWithCountOfMoviesAsync();
            ViewBag.AllGenresWithCountOfMovies = allGenresWithCountMovies;

            return View(movieModelView);              
        }

        public async Task<IActionResult> Index()
        {
            var allGenres = await cache.GetOrCreateAsync<IReadOnlyCollection<string>>("Genres", async (cacheEntry) =>
            {
                var genres = (await this.genreService.GetAllGenresAsync());
                cacheEntry.SlidingExpiration = TimeSpan.FromDays(1);
                return genres;
            });

            var countOfGenres = allGenres.Count();

            var allGenresWithCountMovies = await genreService.GetAllGenresWithCountOfMoviesAsync();                       

            ViewBag.AllGenres = allGenres;
            ViewBag.CountOfGenres = countOfGenres;
            ViewBag.AllGenresWithCountOfMovies = allGenresWithCountMovies;

            return View();
        }
    }
}