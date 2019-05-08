using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Services
{
    public class MovieService : IMovieService
    {
        private readonly MoviesCatalogContext context;

        public MovieService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Movie> CreateMovie(string title, string trailer, string poster, string description, DateTime releaseDate,string userId)
        {
            var user = this.context.Users.Find(userId);

            var movie = await this.context.Movies.FirstOrDefaultAsync(t => t.Title == title);

            if (movie != null)
            {
                throw new ArgumentException();
            }

            movie = new Movie() { Title = title, Trailer = trailer, Poster = poster, Description = description, ReleaseDate = releaseDate};

            movie.User = user;

            this.context.Movies.Add(movie);
            this.context.SaveChanges();

            return movie;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowAllMoviesOrderedDescByRating()
        {
            var movies = await this.context.Movies
                             .OrderByDescending(ar => ar.AverageRating)
                             .ToListAsync();

            return movies;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesTop10ByRaiting()
        {
            var movies = await this.context.Movies
                             .OrderByDescending(ar => ar.AverageRating)
                             .Take(10)
                             .ToListAsync();

            return movies;
        }


        public async Task<IReadOnlyCollection<Movie>> ShowMoviesLatest6ByReleaseDate()
        {
            var movies = await this.context.Movies
                             .OrderByDescending(rd => rd.ReleaseDate)
                             .Take(6)
                             .ToListAsync();

            return movies;
        }

        public async Task<IReadOnlyCollection<Movie>> SearchMoviesContainsString(string criteria)
        {
            var movies = await this.context.Movies
                             .Where(t => t.Title.Contains(criteria))
                             .OrderByDescending(rd => rd.ReleaseDate)
                             .ToListAsync();

            return movies;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesStartWithSymbol(char symbol)
        {
            var movies = await this.context.Movies
                                     .Where(t => t.Title.ToLower().StartsWith(symbol.ToString().ToLower()))
                                     .ToListAsync();

            return movies;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await this.context.Movies.FindAsync(id);

            return movie;
        }

        public async Task<ICollection<Review>> AllReviewsByMovie(int movieId)
        {
            var reviews = await context.Reviews
                                .Where(x => x.Movie.Id == movieId && !x.IsDeleted)
                                .OrderByDescending(co => co.CreatedOn)
                                .Include(x => x.User)
                                .ToListAsync();
            return reviews;
        }

        public async Task<ICollection<Review>> LastFiveReviewsByMovie(int movieId)
        {
            var reviews = await context.Reviews
                                .Where(x => x.Movie.Id == movieId && !x.IsDeleted)
                                .OrderByDescending(co => co.CreatedOn)
                                .Include(x => x.User)
                                .Take(5)
                                .ToListAsync();
            return reviews;
        }

        public async Task<bool> IsMovieExist(string movieTitle)
        {
            return await this.context.Movies
                                    .AnyAsync(t => t.Title == movieTitle);
        }

    }
}
