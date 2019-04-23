using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalog.Services.Contracts
{
    public interface IMovieService
    {
        ICollection<Movie> ShowMoviesStartWithSymbol(char symbol);

        ICollection<Movie> ShowMoviesTop10ByRaiting();

        ICollection<Movie> ShowMoviesLatest10ByReleaseDate();
    }
}
