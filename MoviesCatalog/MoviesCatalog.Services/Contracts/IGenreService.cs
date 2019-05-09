using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IGenreService
    {                    
        Task<IReadOnlyCollection<string>> GetAllGenresAsync();

        Task<IReadOnlyCollection<Movie>> ShowMoviesByGenreAsync(string id);

        Task<IReadOnlyDictionary<string, int>> GetAllGenresWithCountOfMoviesAsync();

        Task<bool> IsGenreExistAsync(string genreName);

        Task<Genre> CreateGenreAsync(string genreName);
    }
}
