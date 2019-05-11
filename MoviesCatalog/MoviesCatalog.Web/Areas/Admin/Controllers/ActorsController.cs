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
using MoviesCatalog.Web.Models.ManageViewModels;
using MoviesCatalog.Web.Services.Contracts;

namespace MoviesCatalog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ActorsController : Controller
    {
        private readonly IActorService actorService;
        private readonly IMovieService movieService;
        private readonly IImageOptimizer optimizer;
        private readonly IViewModelMapper<Actor, ActorViewModel> actorMapper;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieMapper;

        public ActorsController(IActorService actorService,
                                IMovieService movieService,
                                IImageOptimizer optimizer,
                                IViewModelMapper<Actor, ActorViewModel> actorMapper,
                                IViewModelMapper<Movie, MovieViewModel> movieMapper)
        {
            this.actorService = actorService ?? throw new ArgumentNullException(nameof(actorService));
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.optimizer = optimizer ?? throw new ArgumentNullException(nameof(optimizer));
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
                string imageName = null;

                if (model.ActorPicture != null)
                {
                    imageName = optimizer.OptimizeImage(model.ActorPicture, 268, 182);
                }

                var actor = await this.actorService.FindActorByNameAsync(model.FirstName, model.LastName);
                if (actor != null)
                {
                    StatusMessage = $"Actor \"{model.FirstName} {model.LastName}\" already exists.";
                    return RedirectToAction("Create", "Actors");
                }
                    actor = await this.actorService
                                    .CreateActorAsync(model.FirstName, model.LastName, imageName, model.Biography);

                if (actor.FirstName == model.FirstName && actor.LastName == model.LastName 
                    && actor.Picture == imageName && actor.Biography == model.Biography)
                {
                    StatusMessage = $"Successfully added \"{model.FirstName} {model.LastName}\".";
                    
                }
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
            var actor = await this.actorService.GetActorByIdAsync(id);
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
                var actor = await this.actorService.GetActorByIdAsync(model.Id);
                if (actor == null)
                {
                    return NotFound();
                }

                string imageName = null;

                if (model.ActorPicture != null)
                {
                    imageName = optimizer.OptimizeImage(model.ActorPicture, 268, 182);
                }

                if (model.Picture != null)
                {
                    optimizer.DeleteOldImage(model.Picture);
                }

                actor = await this.actorService
                                  .UpdateActorAsync(actor, imageName, model.Biography);

                if (actor.FirstName == model.FirstName && actor.LastName == model.LastName &&
                    actor.Picture == model.Picture && actor.Biography == model.Biography)
                {
                    StatusMessage = $"Successfully updated \"{model.FirstName} {model.LastName}\" details.";
                }
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
            var actor = await this.actorService.GetActorByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            var movies = await this.movieService.ShowAllMoviesOrderedDescByRatingAsync();
            var movieViewModel = movies.Select(this.movieMapper.MapFrom);
            ViewData["ActorId"] = id;
            return View(movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToMovie(int movieId, int actorId)
        {
            try
            {
                var movie = await this.movieService.GetMovieByIdAsync(movieId);
                if (movie == null)
                {
                    return NotFound();
                }
                var actor = await this.actorService.GetActorByIdAsync(actorId);
                if (actor == null)
                {
                    return NotFound();
                }

                await this.actorService.AddActorToMovieAsync(movie.Id, actor.Id);
                StatusMessage = $"Successfully added \"{actor.FirstName} {actor.LastName}\" to \"{movie.Title}\".";

                return RedirectToAction("Details", "Actors", new { id = actorId });
            }
            catch (ArgumentException ex)
            {
                StatusMessage = ex.Message;

                return RedirectToAction("Details", "Actors", new { id = actorId });
            }
        }
    }
}