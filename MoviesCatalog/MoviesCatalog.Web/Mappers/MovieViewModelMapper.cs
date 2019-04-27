using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Mappers
{
    public class MovieViewModelMapper :IViewModelMapper<Movie, MovieViewModel>
    {
        public MovieViewModel MapFrom(Movie entity)
        {
            return new MovieViewModel()
            {
                Id = entity.Id,
                Title = entity.Title,                
                Trailer = entity.Trailer,
                Poster = entity.Poster,
                Description = entity.Description,
                ReleaseDate = entity.ReleaseDate,
                //User = entity.User.UserName,
                //NumberOfVotes = entity.NumberOfVotes,
                //AverageRating = entity.AverageRating
            };
        }
    }
}
