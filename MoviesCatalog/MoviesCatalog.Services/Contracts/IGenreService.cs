using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IGenreService
    {
        Genre GetGenre(int id);

        IReadOnlyCollection<string> GetAllGenres();

        //IReadOnlyCollection<Movie> ShowMoviesByGenre(string id);
        Task<IReadOnlyCollection<Movie>> ShowMoviesByGenre(string id);

        //Task<IReadOnlyDictionary<string, int>> GetAllGenresWithCountOfMovies(); 
        IReadOnlyDictionary<string, int> GetAllGenresWithCountOfMovies();
    }
}
