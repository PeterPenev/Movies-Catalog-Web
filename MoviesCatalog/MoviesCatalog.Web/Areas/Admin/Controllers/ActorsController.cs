using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await this.actorService.IsActorExistAsync(model.FirstName, model.LastName))
                {
                    StatusMessage = $"Actor \"{model.FirstName} {model.LastName}\" already exists.";
                    return RedirectToAction("Create", "Actors");
                }
                    StatusMessage = $"Successfully added \"{model.FirstName} {model.LastName}\".";
                    var actor = await this.actorService
                                    .CreateActorAsync(model.FirstName, model.LastName, model.Biography);
                    return RedirectToAction("Details", "Actors", new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var actor = await this.actorService.GetActorAsync(id);
            var actorViewModel = this.actorMapper.MapFrom(actor);
            return View(actorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ActorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var actor = await this.actorService
                                .UpdateActorBiographyAsync(model.Id, model.Biography);

                StatusMessage = $"Successfully updated {model.FirstName} {model.LastName} details.";
                return RedirectToAction("Details", "Actors", new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task <IActionResult> AddToMovie(int id)
        {
            var actor = await this.actorService.GetActorAsync(id);
            var movies = await this.movieService.ShowAllMoviesOrderedDescByRating();
            var movieViewModel = movies.Select(this.movieMapper.MapFrom);
            return View(movieViewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddToMovie(MovieViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    try
        //    {
        //        //var actor = await this.actorService
        //        //                .UpdateActorBiographyAsync(model.Id, model.Biography);

        //        //StatusMessage = $"Successfully updated {model.FirstName} {model.LastName} details.";
        //        //return RedirectToAction("Details", "Actors", new { id = actor.Id });
        //        return View();
        //    }

        //    catch (ArgumentException ex)
        //    {
        //        this.ModelState.AddModelError("Error", ex.Message);
        //        return View(model);
        //    }
        //}
    }
}