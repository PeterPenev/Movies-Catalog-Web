using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IMovieService
    {
        Task<Movie> CreateMovie(string title, string trailer, string poster, string description, DateTime releaseDate, string UserName);
        //Task<Movie> CreateMovie(string title, string trailer, string poster, string description, DateTime releaseDate);

        Task<IReadOnlyCollection<Movie>> ShowMoviesStartWithSymbol(char symbol);
        
        Task<IReadOnlyCollection<Movie>> ShowAllMoviesOrderedDescByRating();

        Task<IReadOnlyCollection<Movie>> ShowMoviesTop10ByRaiting();

        Task<IReadOnlyCollection<Movie>> ShowMoviesLatest6ByReleaseDate();
        
        Task<IReadOnlyCollection<Movie>> SearchMoviesContainsString(string criteria);

        Task<Movie> GetMovieById(int id);
       
        Task<ICollection<Review>> AllReviewsByMovie(int movieId);

        Task<ICollection<Review>> LastFiveReviewsByMovie(int movieId);

        Task<bool> IsMovieExist(string movieTitle);

        Task<IReadOnlyCollection<Movie>> ShowMoviesTop10ByRaitingContainsSliderImage();

        Task<Movie> UpdateMovie(Movie movie, string description, string poster, string sliderImage);

        Task<ICollection<Actor>> AllActorsByMovie(int movieId);

        Task<ICollection<Genre>> AllGenresByMovie(int movieId);
    }
}
