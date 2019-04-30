﻿using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IMovieService
    {
        Movie CreateMovie(string title, string trailer, string poster, string description, DateTime releaseDate);
       
        Task<IReadOnlyCollection<Movie>> ShowMoviesStartWithSymbol(int id);

        IReadOnlyCollection<Movie> ShowMoviesTop10ByRaiting();

        IReadOnlyCollection<Movie> ShowMoviesLatest10ByReleaseDate();

        IReadOnlyCollection<Movie> SearchMoviesContainsString(string criteria);

        Movie GetMovieById(int id);
    }
}
