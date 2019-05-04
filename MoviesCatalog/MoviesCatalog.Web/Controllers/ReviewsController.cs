using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Extensions;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IMovieService movieService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IViewModelMapper<Review, ReviewViewModel> reviewMapper;

        public ReviewsController(IReviewService reviewService,
                                 IMovieService movieService,
                                 UserManager<ApplicationUser> userManager,
                                 IViewModelMapper<Review,ReviewViewModel> reviewMapper)
        {
            this.reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.userManager = userManager;
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.reviewMapper = reviewMapper ?? throw new ArgumentNullException(nameof(reviewMapper));
        }

        public async Task<IActionResult> Details(int id)
        {
            var review = await this.reviewService.GetReview(id);
            var userId = this.User.GetId();

            var reviewViewModel = this.reviewMapper.MapFrom(review);
            reviewViewModel.CanUserEdit = review.UserId == userId;

            return View(reviewViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var userId = this.userManager.GetUserId(HttpContext.User);
            //var movie = this.movieService.GetMovieById(movieId);
            var reviewViewModel = new ReviewViewModel()
            {
                MovieId = id,
                UserId = userId
            };

            return View(reviewViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var review = this.reviewService
                                .AddReviewToMovie(model.MovieId, model.UserId, model.Description, model.Rating);


                return RedirectToAction("Details", "Movies", new { id = review.MovieId });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }
    }
}