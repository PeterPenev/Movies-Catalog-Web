using Microsoft.AspNetCore.Authorization;
﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using MoviesCatalog.Web.Services.Contracts;
using MoviesCatalog.Web.Utils;

namespace MoviesCatalog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IViewModelMapper<Movie, MovieViewModel> movieViewMapper;
        private readonly IActorService actorService;
        private readonly IViewModelMapper<Actor, ActorViewModel> actorViewMapper;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IImageOptimizer optimizer;

        public MoviesController(IMovieService movieService,
                                IViewModelMapper<Movie, MovieViewModel> movieViewMapper,
                                IActorService actorService,
                                IViewModelMapper<Actor, ActorViewModel> actorViewMapper,
                                IImageOptimizer optimizer,
                                SignInManager<ApplicationUser> signInManager)
        {
            this.optimizer = optimizer;
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.movieViewMapper = movieViewMapper ?? throw new ArgumentNullException(nameof(movieViewMapper));
            this.actorService = actorService ?? throw new ArgumentNullException(nameof(actorService));
            this.actorViewMapper = actorViewMapper ?? throw new ArgumentNullException(nameof(actorViewMapper));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["UserId"] = signInManager.UserManager.Users.First(u => u.UserName == User.Identity.Name).Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await this.movieService.IsMovieExistAsync(model.Title))
                {
                    StatusMessage = string.Format(WebConstants.MovieAlreadyExists, model.Title);

                    return RedirectToAction("Create", "Movies");
                }

                StatusMessage = string.Format(WebConstants.MovieSuccessfullyCreated, model.Title);

                string posterName = null;
                string sliderName = null;

                if (model.PosterImage != null)
                {
                    posterName = optimizer.OptimizeImage(model.PosterImage, 268, 182);
                }

                if (model.SliderPoster != null)
                {
                    sliderName = optimizer.OptimizeImage(model.SliderPoster, 500, 1300);
                }

                var movie = await this.movieService
                        .CreateMovieAsync(model.Title, model.Trailer, posterName, sliderName, model.Description, model.ReleaseDate, model.UserId);

                return RedirectToAction("Details", "Movies", new { id = movie.Id });
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
            var movie = await this.movieService.GetMovieByIdAsync(id);
            var movieViewModel = this.movieViewMapper.MapFrom(movie);
            return View(movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MovieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var movie = await this.movieService.GetMovieByIdAsync(model.Id);
                if (movie == null)
                {
                    return NotFound();
                }

                string posterName = null;
                string sliderName = null;

                if (model.PosterImage != null)
                {
                    posterName = optimizer.OptimizeImage(model.PosterImage, 268, 182);
                }

                if (model.SliderPoster != null)
                {
                    sliderName = optimizer.OptimizeImage(model.SliderPoster, 500, 1300);
                }
                if (model.Poster != null)
                {
                    optimizer.DeleteOldImage(model.Poster);
                }
                if (model.SliderImage != null)
                {
                    optimizer.DeleteOldImage(model.SliderImage);
                }

                movie = await this.movieService
                                  .UpdateMovieAsync(movie, model.Description, posterName, sliderName);

                if (movie.Description == model.Description && movie.Poster == posterName && movie.SliderImage == sliderName)
                {
                    StatusMessage = string.Format(WebConstants.MovieSuccessfullyUpdated, model.Title);                    
                }
                return RedirectToAction("Details", "Movies", new { id = movie.Id });

            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddActorToMovie(int id)
        {
            var actors = await this.actorService.ShowAllActorsAsync();

            if (actors == null)
            {
                return NotFound();
            }

            var actorViewModel = actors.Select(this.actorViewMapper.MapFrom).ToList();

            ViewData["MovieId"] = id;
            return View(actorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActorToMovie(int actorId, int movieId)
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

                StatusMessage = string.Format(WebConstants.SuccessfullyAddActorToMovie, actor.FirstName, actor.LastName, movie.Title);
               
                return RedirectToAction("Details", "Movies", new { id = movie.Id });
            }
            catch (ArgumentException ex)
            {
                StatusMessage = ex.Message;

                return RedirectToAction("AddActorToMovie", "Movies");
            }
        }
    }
}