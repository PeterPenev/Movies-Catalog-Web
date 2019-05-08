using MoviesCatalog.Data.Models;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IReviewService
    {
        Task<Review> GetReviewById(int id);

        Task<Review> AddReviewToMovie(int movieId, string userId,
                                     string description, double rating);

        Task<Review> DeleteReviewAsync(int reviewId);

        Task<bool> DidUserAlreadyVoteForMovieAsync(int movieId, string userId);

        Task<Review> EditReviewAsync(Review review, double rating, string description);
    }
}
