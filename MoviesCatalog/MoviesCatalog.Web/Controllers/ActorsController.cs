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
        private readonly IMovieService movieService;
        private readonly IViewModelMapper<Actor, ActorViewModel> actorMapper;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieMapper;

        public ActorsController(IActorService actorService,
                                IMovieService movieService,
                                IViewModelMapper<Actor, ActorViewModel> actorMapper,
                                IViewModelMapper<Movie, MovieViewModel> movieMapper)
        {
            this.actorService = actorService ?? throw new ArgumentNullException(nameof(actorService));
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
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
            var topRatedMovies = this.movieService.ShowMoviesTop10ByRaiting();
            if (actor == null)
            {
                return NotFound();
            }

            var actorViewModel = this.actorMapper.MapFrom(actor);
            actorViewModel.TopRatedMovies = topRatedMovies.Select(this.movieMapper.MapFrom).ToList();

            return View(actorViewModel);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create(ActorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var actor = this.actorService
                                .CreateActorAsync(model.FirstName, model.LastName, model.Biography);


                return RedirectToAction(nameof(Index), new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var viewModel = await this.actorService.GetActorAsync(id);
            return View(actorMapper.MapFrom(viewModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(ActorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var actor = this.actorService
                                .UpdateActorBiographyAsync(model.Id, model.Biography);


                return RedirectToAction(nameof(Index), new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }
    }
}