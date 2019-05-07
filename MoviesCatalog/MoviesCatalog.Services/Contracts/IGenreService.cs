using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IGenreService
    {                    
        Task<IReadOnlyCollection<string>> GetAllGenres();

        Task<IReadOnlyCollection<Movie>> ShowMoviesByGenre(string id);

        Task<IReadOnlyDictionary<string, int>> GetAllGenresWithCountOfMovies();        
    }
}
