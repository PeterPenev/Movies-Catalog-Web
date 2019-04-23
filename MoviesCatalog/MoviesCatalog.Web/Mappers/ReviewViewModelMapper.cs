using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Mappers
{
    public class ReviewViewModelMapper : IViewModelMapper<Review, ReviewViewModel>
    {
        public ReviewViewModel MapFrom(Review entity)
        {
            return new ReviewViewModel()
            {
                Description = entity.Description,
                Rating = entity.Rating,
                CreatedOn = entity.CreatedOn,
                UserName = entity.User.UserName,
                MovieTitle = entity.Movie.Title
            };
        }
    }
}
