using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using MoviesCatalog.Web.Models.AccountViewModels;

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
            var showTo10Actors = await this.actorService.ShowAllActors();

            var actorIndexView = new ActorIndexViewModel()
            {
                AllActors = showTo10Actors.Select(this.actorMapper.MapFrom).ToList()
            };
            return View(actorIndexView);
        }

        public async Task<IActionResult> ActorsByName(string id)
        {
            var actorsByStartingSymbol = await this.actorService.ShowActorsStartWithSymbolAsync(id);

            var actorIndexView = new ActorIndexViewModel()
            {
                ActorsByName = actorsByStartingSymbol.Select(this.actorMapper.MapFrom).ToList()
            };

            return View(actorIndexView);
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await this.actorService.GetActorAsync(id);

            var actorMovies = await this.actorService.ShowLastFiveActorMovies(actor.Id);

            var actorViewModel = this.actorMapper.MapFrom(actor);
            actorViewModel.MoviesByActor = actorMovies.Select(this.movieMapper.MapFrom).ToList();

            return View(actorViewModel);
        }
    }
}