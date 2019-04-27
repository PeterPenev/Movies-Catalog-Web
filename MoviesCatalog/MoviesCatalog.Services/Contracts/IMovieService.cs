using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalog.Services.Contracts
{
    public interface IMovieService
    {
        Movie CreateMovie(string title, string trailer, string poster, string description, DateTime releaseDate);

        IReadOnlyCollection<Movie> ShowMoviesStartWithSymbol(char symbol);

        IReadOnlyCollection<Movie> ShowMoviesTop10ByRaiting();

        IReadOnlyCollection<Movie> ShowMoviesLatest10ByReleaseDate();
    }
}
