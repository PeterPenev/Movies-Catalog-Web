using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Extensions;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using MoviesCatalog.Web.Utils;
using System;
using System.Threading.Tasks;

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
            var reviewViewModel = this.reviewMapper.MapFrom(review);
            reviewViewModel.CanUserEdit = review.UserId == userId;

            return View(reviewViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var userId = this.User.GetId();
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
                    StatusMessage = WebConstants.UserAlreadyVoted;
                    return RedirectToAction("Details", "Movies", new { id = model.MovieId });
                }

                var review = await this.reviewService
                                .AddReviewToMovieAsync(model.MovieId, model.UserId, model.Description, model.Rating);
                StatusMessage = WebConstants.ReviewAddedToMovie;
                return RedirectToAction("Details", "Reviews", new { id = review.Id });

            }

            catch (ArgumentException ex)
            {
                StatusMessage = ex.Message;
                return RedirectToAction("Details", "Movies", new { id = model.MovieId });
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
                var userId = this.User.GetId();
                review = await this.reviewService
                                    .EditReviewAsync(review, userId, model.Rating, model.Description);

                if (review.Description == model.Description && review.Rating == model.Rating)
                {
                    StatusMessage = WebConstants.ReviewEdited;
                }

                return RedirectToAction("Details", "Reviews", new { id = review.Id });
            }

            catch (ArgumentException ex)
            {
                StatusMessage = ex.Message;
                return RedirectToAction("Details", "Reviews", new { id = model.Id });
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
            StatusMessage = WebConstants.ReviewDeleted;
            return RedirectToAction( "Details", "Movies", new { id = review.MovieId });
        }
    }
}