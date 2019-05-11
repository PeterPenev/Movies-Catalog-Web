using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService actorService;
        private readonly IViewModelMapper<Actor, ActorViewModel> actorMapper;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieMapper;

        public ActorsController(IActorService actorService,
                                IViewModelMapper<Actor, ActorViewModel> actorMapper,
                                IViewModelMapper<Movie, MovieViewModel> movieMapper)
        {
            this.actorService = actorService ?? throw new ArgumentNullException(nameof(actorService));
            this.actorMapper = actorMapper ?? throw new ArgumentNullException(nameof(actorMapper));
            this.movieMapper = movieMapper ?? throw new ArgumentNullException(nameof(movieMapper));
        }

        public async Task<IActionResult> Index()
        {
            var actors = await this.actorService.ShowAllActorsAsync();
            var actorViewModel = actors.Select(this.actorMapper.MapFrom).ToList();
            return View(actorViewModel);
        }

        public async Task<IActionResult> ActorsByName(char id)
        {
            var actors = await this.actorService.ShowActorsStartWithSymbolAsync(id);
            var actorViewModel = actors.Select(this.actorMapper.MapFrom).ToList();
            return View(actorViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await this.actorService.GetActorByIdAsync(id);

            var actorMovies = await this.actorService.ShowLastFiveActorMoviesAsync(actor.Id);

            var actorViewModel = this.actorMapper.MapFrom(actor);
            actorViewModel.LastFiveMoviesByActor = actorMovies.Select(this.movieMapper.MapFrom).ToList();

            return View(actorViewModel);
        }
    }
}