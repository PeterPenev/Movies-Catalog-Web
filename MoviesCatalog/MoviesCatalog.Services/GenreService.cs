using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Services
{
    public class GenreService : IGenreService
    {
        private readonly MoviesCatalogContext context;

        public GenreService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyDictionary<string, int>> GetAllGenresWithCountOfMoviesAsync()
        {
            var genresCountMovies = new Dictionary<string, int>();

            var genres = await this.context.Genres
                .OrderBy(gn => gn.Name)
                .Select(gn => gn.Name)
                .ToListAsync();

            foreach (var genre in genres)
            {
                var countOfMoviesForGenre = this.context
                    .MoviesGenres
                    .Where(x => x.Genre.Name == genre)
                    .Distinct()
                    .Count();

                genresCountMovies.Add(genre, countOfMoviesForGenre);
            }

            return genresCountMovies;
        }

        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            var genre = await this.context.Genres
                                  .FindAsync(genreId);
                
            return genre;
        }        

        public async Task<IReadOnlyCollection<Genre>> GetAllGenresAsync()
        {
            var genres = await this.context
                .Genres
                .ToListAsync();

            return genres;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesByGenreAsync(string id)
        {
            var movies = await this.context.Movies
                             .Where(m => m.MoviesGenres.Any(mg => mg.Genre.Name == id))
                             .ToListAsync();

            return movies;
        }

        public async Task<bool> IsGenreExistAsync(string genreName)
        {
            return await this.context.Genres
                                    .AnyAsync(gn => gn.Name == genreName);
        }

        public async Task<Genre> CreateGenreAsync(string genreName)
        {
            var genre = new Genre() { Name = genreName };

            this.context.Genres.Add(genre);
            await this.context.SaveChangesAsync();

            return genre;
        }

        public async Task<Movie> AddGenreToMovieAsync(int movieId, int genreId)
        {
            var movie = await this.context.Movies.Include(m => m.MoviesGenres).FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie.MoviesGenres.Any(g => g.GenreId == genreId))
            {
                throw new ArgumentException(string.Format(ServicesConstants.GenreIsInMovie));
            }

            await this.context.MoviesGenres.AddAsync(new MoviesGenres() { MovieId = movie.Id, GenreId = genreId });

            await this.context.SaveChangesAsync();

            return movie;
        }
    }
}
