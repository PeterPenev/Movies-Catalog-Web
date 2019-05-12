using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System.Linq;

namespace MoviesCatalog.Web.Mappers
{
    public class UserViewModelMapper: IViewModelMapper<ApplicationUser, UserViewModel>
    {
        public UserViewModel MapFrom(ApplicationUser entity)
        {
            return new UserViewModel()
            {
                Id = entity.Id,
                Email = entity.Email,
                UserName = entity.UserName,
                NumberOfReviews = entity.Reviews.Count()
            };
        }
    }
}
