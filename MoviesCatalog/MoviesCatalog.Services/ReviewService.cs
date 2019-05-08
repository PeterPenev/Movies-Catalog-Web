﻿using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
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

        public async Task<Review> GetReviewById(int id)
        {
            var review = await this.context.Reviews
                                           .Where(x => x.Id == id)
                                           .Include(x => x.Movie)
                                           .Include(x => x.User)
                                           .FirstOrDefaultAsync();

            //if (review == null)
            //{
            //    throw new ArgumentException(string.Format(ServicesConstants.ReviewNotPresent));
            //}

            return review;
        }

        public async Task<bool> DidUserAlreadyVoteForMovieAsync(int movieId, string userId)
        {
            return await this.context.Reviews.AnyAsync(x => x.UserId == userId && x.MovieId == movieId && !x.IsDeleted);
        }

        public async Task<Review> AddReviewToMovie(int movieId, string userId,
                                    string description, double rating)
        {
            var review = await this.context.Reviews.Where(x => x.UserId == userId && x.MovieId == movieId)
                                             .Include(x => x.Movie)
                                             .Include(x => x.User)
                                             .FirstOrDefaultAsync();
          

            var movie = await this.context.Movies.FindAsync(movieId);
            var user =  await this.context.Users.FindAsync(userId);

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

        public async Task<Review> DeleteReviewAsync(int reviewId, string userId)
        {
            var review = await this.context.Reviews.Where(x => x.Id == reviewId && !x.IsDeleted)
                                     .Include(x => x.Movie)
                                     .FirstOrDefaultAsync();
            if (review == null)
            {
                throw new ArgumentException(string.Format(ServicesConstants.ReviewNotPresent));
            }

            var user = this.context.Users.Find(userId);

            if (review.UserId != user.Id)
            {
                throw new ArgumentException(string.Format(ServicesConstants.ReviewNotFromUser,
                                                          user.UserName));
            }

            var movie = review.Movie;
            review.IsDeleted = true;
            movie.TotalRating -= review.Rating;

            await context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> EditReviewAsync(Review review, string userId, double rating, string description)
        {
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
            movie.TotalRating -= review.Rating;
            review.Rating = rating;
            movie.TotalRating += rating;

            await this.context.SaveChangesAsync();
            return review;
        }
    }
}
