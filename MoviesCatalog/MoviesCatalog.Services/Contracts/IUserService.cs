using MoviesCatalog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);

        Task<IReadOnlyCollection<ApplicationUser>> ShowUsersStartWithSymbolAsync(string symbol);

        Task<IReadOnlyCollection<ApplicationUser>> ShowAllUsersAsync();

        Task<ICollection<Review>> ShowUserLastFiveReviewsAsync(string userId);

        Task<ICollection<Review>> ShowUserReviewsAsync(string userId);

        Task AddRoleAsync(ApplicationUser user);
    }
}
