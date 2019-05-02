using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalog.Services.Contracts
{
    public interface IGenreService
    {
        Genre GetGenre(int id);

        IReadOnlyCollection<string> GetAllGenres();

        IReadOnlyCollection<Movie> ShowMoviesByGenre(string id);             

        IReadOnlyDictionary<string, int> GetAllGenresWithCountOfMovies();
    }
}
