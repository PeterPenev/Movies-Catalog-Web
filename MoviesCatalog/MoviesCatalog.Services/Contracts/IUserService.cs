using MoviesCatalog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string id);

        ApplicationUser CreateUser(string userName, string password, string email);

        Task<IReadOnlyCollection<ApplicationUser>> ShowUsersStartWithSymbolAsync(int id);

        Task<IReadOnlyCollection<ApplicationUser>> ShowTenUsers();
    }
}
