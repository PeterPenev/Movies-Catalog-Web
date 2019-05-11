using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Mappers
{
    public class ActorViewModelMapper : IViewModelMapper<Actor, ActorViewModel>
    {
        public ActorViewModel MapFrom(Actor entity)
        {
            return new ActorViewModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Biography = entity.Biography,
                Picture = entity.Picture,
            };
        }
    }
}
