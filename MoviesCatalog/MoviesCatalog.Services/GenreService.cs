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
    public class GenreService : IGenreService
    {
        private readonly MoviesCatalogContext context;

        public GenreService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyDictionary<string, int>> GetAllGenresWithCountOfMovies()
        {
            var genresCountMovies = new Dictionary<string, int>();

            var genres = await this.context.Genres.OrderBy(gn => gn.Name).Select(gn => gn.Name).ToListAsync();

            foreach (var genre in genres)
            {
                var countOfMoviesForGenre = this.context.MoviesGenres.Where(x => x.Genre.Name == genre).Distinct().Count();
                genresCountMovies.Add(genre, countOfMoviesForGenre);
            }

            return genresCountMovies;
        }

        public async Task<IReadOnlyCollection<string>> GetAllGenres()
        {
            var genres = await this.context.Genres.Select(gn => gn.Name).ToListAsync();

            return genres;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesByGenre(string id)
        {
            var movies = await this.context.Movies
                             .Where(m => m.MoviesGenres.Any(mg => mg.Genre.Name == id))
                             .ToListAsync();

            return movies;
        }

        public async Task<bool> IsGenreExist(string genreName)
        {
            return await this.context.Genres
                                    .AnyAsync(gn => gn.Name == genreName);
        }

        public async Task<Genre> CreateGenre(string genreName)
        {
            var genre = await this.context.Genres.FirstOrDefaultAsync(g => g.Name == genreName);

            if (genre != null)
            {
                throw new ArgumentException();
            }

            genre = new Genre() { Name = genreName };

            this.context.Genres.Add(genre);
            this.context.SaveChanges();

            return genre;
        }

    }
}
