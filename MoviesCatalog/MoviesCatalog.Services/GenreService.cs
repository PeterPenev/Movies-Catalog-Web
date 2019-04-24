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

        public IReadOnlyCollection<Movie> ShowMoviesByGenre(int genreId)
        {
            Genre genre = context.Genres.Find(genreId);

            var movies = this.context.Movies
                             .Where(m => m.MoviesGenres.Any(mg => mg.Genre.Id == genreId))
                             .ToList();

            return movies;
        }
    }
}
