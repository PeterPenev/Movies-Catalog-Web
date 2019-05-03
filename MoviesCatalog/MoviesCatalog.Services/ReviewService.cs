using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;
using System.Linq;

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

        public Review AddReviewToMovie(int movieId, string userId,
                                    string description, double rating)
        {
            var review = this.context.Reviews.Where(x => x.UserId == userId && x.MovieId == movieId)
                                             .Include(x => x.Movie)
                                             .Include(x => x.User)
                                             .FirstOrDefault();

            var movie = this.context.Movies.Find(movieId);
            var user = this.context.Users.Find(userId);

            if (review == null)
            {
                review = new Review() { Description = description, Rating = rating };
            }

            else if (!review.IsDeleted)
            {
                throw new ArgumentException();
            }

            else if (review.IsDeleted)
            {
                review.Description = description;
                movie.TotalRating -= review.Rating;
                review.Rating = rating;
                movie.NumberOfVotes--;
            }

            review.Movie = movie;
            review.UserId = user.Id;
            review.CreatedOn = DateTime.Now;
            if (review.IsDeleted)
            {
                review.IsDeleted = false;
            }
            else
            {
                context.Reviews.Add(review);
            }

            if (rating > 0)
            {
            movie.NumberOfVotes++;
            movie.TotalRating += rating;
            movie.AverageRating = (double)movie.TotalRating / movie.NumberOfVotes;
            }

            context.SaveChanges();
            return review;
        }
    }
}
