using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesCatalog.Services
{
    public class MovieService : IMovieService
    {
        private readonly MoviesCatalogContext context;

        public MovieService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReadOnlyCollection<Movie> ShowMoviesStartWithSymbol(char symbol)
        {
            var movies = this.context.Movies
                             .Where(t => t.Title.StartsWith(symbol))
                             .ToList();

            return movies;
        }

        public IReadOnlyCollection<Movie> ShowMoviesTop10ByRaiting()
        {
            var movies = this.context.Movies
                             .Take(10)
                             .ToList();

            return movies;
        }

        public IReadOnlyCollection<Movie> ShowMoviesLatest10ByReleaseDate()
        {
            var movies = this.context.Movies
                             .OrderByDescending(rd=>rd.ReleaseDate)   
                             .Take(10)
                             .ToList();

            return movies;
        }
    }
}
