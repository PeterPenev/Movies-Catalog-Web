using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesCatalog.Services
{
    public class GenreService : IGenreService
    {
        private readonly MoviesCatalogContext context;

        public GenreService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Genre GetGenre(int id)
        {
            var genre = this.context.Genres.Find(id);

            return genre;
        }

        public IReadOnlyDictionary<string, int> GetAllGenresWithCountOfMovies()
        {
            var genresCountMovies = new Dictionary<string, int>();

            var genres = this.context.Genres.OrderBy(gn=>gn.Name).Select(gn => gn.Name).ToList();

            foreach (var genre in genres)
            {
                var countOfMoviesForGenre = this.context.MoviesGenres.Where(x => x.Genre.Name == genre).Distinct().Count();
                genresCountMovies.Add(genre, countOfMoviesForGenre);
            }

            return genresCountMovies;
        }

        public IReadOnlyCollection<string> GetAllGenres()
        {
            var genres = this.context.Genres.Select(gn => gn.Name).ToList();

            return genres;
        }

        public IReadOnlyCollection<Movie> ShowMoviesByGenre(string id)
        {
            var movies = this.context.Movies
                             .Where(m => m.MoviesGenres.Any(mg => mg.Genre.Name == id))
                             .ToList();

            return movies;
        }
    }
}
