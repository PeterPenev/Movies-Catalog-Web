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
        private readonly IUserService userService;
        private readonly IViewModelMapper<Review, ReviewViewModel> reviewMapper;

        public ReviewsController(IReviewService reviewService,
                                 IMovieService movieService,
                                 IUserService userService,
                                 UserManager<ApplicationUser> userManager,
                                 IViewModelMapper<Review,ReviewViewModel> reviewMapper)
        {
            this.reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            this.movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.reviewMapper = reviewMapper ?? throw new ArgumentNullException(nameof(reviewMapper));
        }

        [TempData] public string StatusMessage { get; set; }

        public async Task<IActionResult> Details(int id)
        {
            var review = await this.reviewService.GetReviewByIdAsync(id);
            var userId = this.User.GetId();
            var user = await this.userService.GetUserByIdAsync(review.UserId);
            var movie = await this.movieService.GetMovieByIdAsync(review.MovieId);
            //review.Movie.Title = movie.Title;
            //review.Movie.Poster = movie.Poster;
            //review.User.UserName = user.UserName;
           
            var reviewViewModel = this.reviewMapper.MapFrom(review);
            reviewViewModel.CanUserEdit = review.UserId == userId;

            return View(reviewViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var userId = this.User.GetId();
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
        public async Task<IActionResult> Create(ReviewViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await this.reviewService.DidUserAlreadyVoteForMovieAsync(model.MovieId, model.UserId))
                {
                    StatusMessage = "You already voted for this movie.";
                    return RedirectToAction("Details", "Movies", new { id = model.MovieId });
                }

                var review = await this.reviewService
                                .AddReviewToMovieAsync(model.MovieId, model.UserId, model.Description, model.Rating);
                StatusMessage = $"Successfully added review to movie.";
                return RedirectToAction("Details", "Reviews", new { id = review.Id });

            }

            catch (ArgumentException ex)
            {
                StatusMessage = ex.Message;

                return RedirectToAction("Details", "Movies", new { id = model.MovieId });


                //this.ModelState.AddModelError("Error", ex.Message);
                //return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            var review = await this.reviewService.GetReviewByIdAsync(id);
            var reviewViewModel = this.reviewMapper.MapFrom(review);
            return View(reviewViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReviewViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var review = await this.reviewService.GetReviewByIdAsync(model.Id);
                if (review == null)
                {
                    return RedirectToAction("Index");
                }
                
                review = await this.reviewService
                                    .EditReviewAsync(review, model.UserId, model.Rating, model.Description);

                if (review.Description == model.Description && review.Rating == model.Rating)
                {
                    StatusMessage = $"Successfully edited your review.";
                }

                return RedirectToAction("Details", "Reviews", new { id = review.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var review = await reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound(); 
            }
            var userId = this.User.GetId();
            var userViewModel = this.reviewMapper.MapFrom(review);
            userViewModel.CanUserEdit = review.UserId == userId;
            var reviewViewModel = this.reviewMapper.MapFrom(review);
            return View(reviewViewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.GetId();
            var review = await reviewService.DeleteReviewAsync(id, userId);
            
            if (review == null)
            {
                return NotFound();
            }
            StatusMessage = "Successfully deleted the review.";
            return RedirectToAction( "Details", "Movies", new { id = review.MovieId });
        }
    }
}