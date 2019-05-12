using MoviesCatalog.Data.Models;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IReviewService
    {
        Task<Review> GetReviewByIdAsync(int id);

        Task<Review> AddReviewToMovieAsync(int movieId, string userId,
                                     string description, double rating);

        Task<Review> DeleteReviewAsync(int reviewId, string userId);

        Task<bool> DidUserAlreadyVoteForMovieAsync(int movieId, string userId);

        Task<Review> EditReviewAsync(Review review, string userId, double rating, string description);
    }
}
