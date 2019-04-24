using MoviesCatalog.Data.Models;

namespace MoviesCatalog.Services.Contracts
{
    public interface IUserService
    {
        ApplicationUser GetUser(int id);

        ApplicationUser CreateUser(string userName, string password, string email);
    }
}
