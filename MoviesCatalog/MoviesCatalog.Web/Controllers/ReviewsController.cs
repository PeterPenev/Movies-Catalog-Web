using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IViewModelMapper<Review, ReviewViewModel> reviewMapper;

        public ReviewsController(IReviewService reviewService,
                                IViewModelMapper<Review,ReviewViewModel> reviewMapper)
        {
            this.reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
            this.reviewMapper = reviewMapper ?? throw new ArgumentNullException(nameof(reviewMapper));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
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

                return RedirectToAction("Movies", "Details", new { id = review.MovieId });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

    }
}