using MoviesCatalog.Data.Models;

namespace MoviesCatalog.Services.Contracts
{
    public interface IReviewService
    {
        Review GetReview(int id);

        Review AddReviewToMovie(int movieId, string userId,
                                    string description, int rating);
    }


}
