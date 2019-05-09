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

        public async Task<Movie> CreateMovieAsync(string title, string trailer, string poster, string description, DateTime releaseDate, string userId)
        {
            var user = await this.context
                .Users
                .FindAsync(userId);

            var movie = await this.context
                .Movies
                .FirstOrDefaultAsync(t => t.Title == title);

            if (movie != null)
            {
                throw new ArgumentException();
            }

            movie = new Movie() { Title = title, Trailer = trailer, Poster = poster, Description = description, ReleaseDate = releaseDate };

            movie.User = user;

            this.context.Movies.Add(movie);
            await this.context.SaveChangesAsync();

            return movie;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowAllMoviesOrderedDescByRatingAsync()
        {
            var movies = await this.context.Movies
                             .Include(x => x.User)
                             .OrderByDescending(ar => ar.AverageRating)
                             .ToListAsync();

            return movies;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesTop10ByRaitingAsync()
        {
            var movies = await this.context.Movies
                             .OrderByDescending(ar => ar.AverageRating)
                             .Take(10)
                             .ToListAsync();

            return movies;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesTop10ByRaitingContainsSliderImageAsync()
        {
            var movies = await this.context.Movies
                             .Where(si => si.SliderImage != null)
                             .OrderByDescending(ar => ar.AverageRating)
                             .Take(10)
                             .ToListAsync();

            return movies;
        }


        public async Task<IReadOnlyCollection<Movie>> ShowMoviesLatest6ByReleaseDateAsync()
        {
            var movies = await this.context.Movies
                             .OrderByDescending(rd => rd.ReleaseDate)
                             .Take(6)
                             .ToListAsync();

            return movies;
        }

        public async Task<IReadOnlyCollection<Movie>> SearchMoviesContainsStringAsync(string criteria)
        {
            var movies = await this.context.Movies
                             .Where(t => t.Title.Contains(criteria))
                             .OrderByDescending(rd => rd.ReleaseDate)
                             .ToListAsync();

            return movies;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesStartWithSymbolAsync(char symbol)
        {
            var movies = await this.context.Movies
                                     .Where(t => t.Title.ToLower().StartsWith(symbol.ToString().ToLower()))
                                     .ToListAsync();

            return movies;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await this.context
                .Movies
                .FindAsync(id);

            return movie;
        }

        public async Task<ICollection<Review>> AllReviewsByMovieAsync(int movieId)
        {
            var reviews = await context.Reviews
                                .Where(x => x.Movie.Id == movieId && !x.IsDeleted)
                                .OrderByDescending(co => co.CreatedOn)
                                .Include(x => x.User)
                                .ToListAsync();
            return reviews;
        }

        public async Task<ICollection<Review>> LastFiveReviewsByMovieAsync(int movieId)
        {
            var reviews = await context.Reviews
                                .Where(x => x.Movie.Id == movieId && !x.IsDeleted)
                                .OrderByDescending(co => co.CreatedOn)
                                .Include(x => x.User)
                                .Take(5)
                                .ToListAsync();
            return reviews;
        }

        public async Task<bool> IsMovieExistAsync(string movieTitle)
        {
            return await this.context.Movies
                                    .AnyAsync(t => t.Title == movieTitle);
        }

        public async Task<Movie> UpdateMovieAsync(Movie movie, string description, string poster, string sliderImage)
        {
            movie.Description = description;
            movie.Poster = poster;
            movie.SliderImage = sliderImage;

            await this.context.SaveChangesAsync();

            return movie;
        }

        public async Task<ICollection<Genre>> AllGenresByMovieAsync(int movieId)
        {
            var genres = await context.Genres
                                      .Where(m => m.MoviesGenres.Any(mg => mg.Movie.Id == movieId))
                                      .ToListAsync();
            return genres;
        }

        public async Task<ICollection<Actor>> AllActorsByMovieAsync(int movieId)
        {
            var actors = await context.Actors
                                .Where(am => am.ActorMovies.Any(m => m.Movie.Id == movieId))
                                .ToListAsync();                                
                                
            return actors;
        }
    }
}
