using MoviesCatalog.Data.Models;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IReviewService
    {
        Task<Review> GetReviewById(int id);

        Review AddReviewToMovie(int movieId, string userId,
                                    string description, double rating);

        Task<Review> DeleteReviewAsync(int reviewId);
    }


}
