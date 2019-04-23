using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;

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
    }
}
