using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Services.Providers;
using MoviesCatalog.Services.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Services
{
    public class ReviewService : IReviewService
    {
        private readonly MoviesCatalogContext context;

        public ReviewService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            var review = await this.context.Reviews
                                           .Where(x => x.Id == id)
                                           .Include(x => x.Movie)
                                           .Include(x => x.User)
                                           .FirstOrDefaultAsync();

            return review;
        }

        public async Task<bool> DidUserAlreadyVoteForMovieAsync(int movieId, string userId)
        {
            return await this.context.Reviews.AnyAsync(x => x.UserId == userId && x.MovieId == movieId && !x.IsDeleted);
        }

        public async Task<Review> AddReviewToMovieAsync(int movieId, string userId,
                                    string description, double rating)
        {
            BusinessValidator.IsInProperRange(description);
            BusinessValidator.IsRatingInRange(rating);

            var review = await this.context.Reviews.Where(x => x.UserId == userId && x.MovieId == movieId)
                                             .Include(x => x.Movie)
                                             .Include(x => x.User)
                                             .FirstOrDefaultAsync();


            var movie = await this.context.Movies.FindAsync(movieId);
            var user = await this.context.Users.FindAsync(userId);

            if (review == null)
            {
                review = new Review() { Description = description, Rating = rating };
            }

            else if (!review.IsDeleted)
            {
                throw new ArgumentException(string.Format(ServicesConstants.UserAlreadyVoted,
                                                          user.UserName, movie.Title));
            }

            else if (review.IsDeleted)
            {
                review.Description = description;
                movie.TotalRating -= review.Rating;
                review.Rating = rating;
                movie.NumberOfVotes--;
            }

            review.Movie = movie;
            review.MovieId = movie.Id;
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
                if (movie.NumberOfVotes != 0)
                {
                    movie.AverageRating = (double)movie.TotalRating / movie.NumberOfVotes;
                }

            }

            context.SaveChanges();
            return review;
        }

        public async Task<Review> DeleteReviewAsync(int reviewId, string userId)
        {
            var review = await this.context.Reviews.Where(x => x.Id == reviewId && !x.IsDeleted)
                                     .Include(x => x.Movie)
                                     .FirstOrDefaultAsync();

            var user = this.context.Users.Find(userId);

            if (review.UserId != user.Id)
            {
                throw new ArgumentException(string.Format(ServicesConstants.ReviewNotFromUser,
                                                          user.UserName));
            }

            var movie = review.Movie;
            review.IsDeleted = true;
            movie.TotalRating -= review.Rating;
            movie.NumberOfVotes--;
            if (movie.NumberOfVotes != 0)
            {
                movie.AverageRating = (double)movie.TotalRating / movie.NumberOfVotes;
            }
            else
            {
                movie.AverageRating = movie.TotalRating;
            }

            await context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> EditReviewAsync(Review review, string userId, double rating, string description)
        {
            BusinessValidator.IsInProperRange(description);
            BusinessValidator.IsRatingInRange(rating);

            if (review.IsDeleted)
            {
                throw new ArgumentException(ServicesConstants.ReviewNotPresent);
            }

            var user = this.context.Users.Find(userId);

            if (review.UserId != user.Id)
            {
                throw new ArgumentException(string.Format(ServicesConstants.ReviewNotFromUser,
                                                          user.UserName));
            }

            review.Description = description;
            var movie = review.Movie;
            if (rating > 0)
            {
                movie.TotalRating -= review.Rating;
                review.Rating = rating;
                movie.TotalRating += rating;
                movie.AverageRating = (double)movie.TotalRating / movie.NumberOfVotes;
            }

            await this.context.SaveChangesAsync();
            return review;
        }
    }
}
