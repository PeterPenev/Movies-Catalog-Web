using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;

namespace MoviesCatalog.Services
{
    public class ReviewService : IReviewService
    {
        private readonly MoviesCatalogContext context;

        public ReviewService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context)); 
        }

        public Review GetReview(int id)
        {
            var review = this.context.Reviews.Find(id);

            if (review == null)
            {
                throw new ArgumentException();
            }

            return review;
        }
    }
}
