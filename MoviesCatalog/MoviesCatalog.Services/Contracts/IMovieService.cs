using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IMovieService
    {
        Task<Movie> CreateMovieAsync(string title, string trailer, string poster, string description, DateTime releaseDate, string UserName);
        
        Task<IReadOnlyCollection<Movie>> ShowMoviesStartWithSymbolAsync(char symbol);
        
        Task<IReadOnlyCollection<Movie>> ShowAllMoviesOrderedDescByRatingAsync();

        Task<IReadOnlyCollection<Movie>> ShowMoviesTop10ByRaitingAsync();

        Task<IReadOnlyCollection<Movie>> ShowMoviesLatest6ByReleaseDateAsync();
        
        Task<IReadOnlyCollection<Movie>> SearchMoviesContainsStringAsync(string criteria);

        Task<Movie> GetMovieByIdAsync(int id);
       
        Task<ICollection<Review>> AllReviewsByMovieAsync(int movieId);

        Task<ICollection<Review>> LastFiveReviewsByMovieAsync(int movieId);

        Task<bool> IsMovieExistAsync(string movieTitle);

        Task<IReadOnlyCollection<Movie>> ShowMoviesTop10ByRaitingContainsSliderImageAsync();

        Task<Movie> UpdateMovieAsync(Movie movie, string description, string poster, string sliderImage);

        Task<ICollection<Actor>> AllActorsByMovieAsync(int movieId);

        Task<ICollection<Genre>> AllGenresByMovieAsync(int movieId);
    }
}
