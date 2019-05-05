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

        public Movie CreateMovie(string title, string trailer, string poster, string description, DateTime releaseDate)
        {
            //var user = this.context.Users.Find(userId);

            var movie = this.context.Movies.FirstOrDefault(t => t.Title == title);

            if (movie != null)
            {
                throw new ArgumentException();
            }

            movie = new Movie() { Title = title, Trailer = trailer, Poster = poster, Description = description, ReleaseDate = releaseDate, };

            //movie.User = user;

            this.context.Movies.Add(movie);
            this.context.SaveChanges();

            return movie;
        }

        public IReadOnlyCollection<Movie> ShowAllMoviesOrderedDescByRating()
        {
            var movies = this.context.Movies
                             .OrderByDescending(ar => ar.AverageRating)
                             .ToList();

            return movies;
        }

        public IReadOnlyCollection<Movie> ShowMoviesTop10ByRaiting()
        {
            var movies = this.context.Movies
                             .OrderByDescending(ar=>ar.AverageRating)
                             .Take(10)
                             .ToList();

            return movies;
        }
        

        public IReadOnlyCollection<Movie> ShowMoviesLatest10ByReleaseDate()
        {
            var movies = this.context.Movies
                             .OrderByDescending(rd => rd.ReleaseDate)
                             .Take(10)
                             .ToList();

            return movies;
        }

        public IReadOnlyCollection<Movie> SearchMoviesContainsString(string criteria)
        {
            var movies = this.context.Movies
                             .Where(t => t.Title.Contains(criteria))
                             .OrderByDescending(rd => rd.ReleaseDate)
                             .ToList();

            return movies;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesStartWithSymbol(char symbol)
        {
            var movies = await this.context.Movies
                                     .Where(t => t.Title.ToLower().StartsWith(symbol.ToString().ToLower()))
                                     .ToListAsync();

            return movies;
        }

        public Movie GetMovieById(int id)
        {
            var movie = this.context.Movies.Find(id);

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
    }
}
