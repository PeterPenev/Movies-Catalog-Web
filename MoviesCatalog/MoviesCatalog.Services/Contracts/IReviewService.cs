using MoviesCatalog.Data.Models;

namespace MoviesCatalog.Services.Contracts
{
    public interface IReviewService
    {
        Review GetReview(int id);
    }
}
