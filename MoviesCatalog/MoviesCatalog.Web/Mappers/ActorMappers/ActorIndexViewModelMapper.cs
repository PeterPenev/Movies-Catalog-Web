using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Mappers.ActorMappers
{
    public class ActorIndexViewModelMapper : IViewModelMapper<IReadOnlyCollection<Actor>, ActorIndexViewModel>
    {
        private readonly IViewModelMapper<Actor, ActorViewModel> actorMapper;


        public ActorIndexViewModelMapper(IViewModelMapper<Actor, ActorViewModel> actorMapper)
        {
            this.actorMapper = actorMapper ?? throw new ArgumentNullException(nameof(actorMapper));
        }

        public ActorIndexViewModel MapFrom(IReadOnlyCollection<Actor> entity)
        {
            return new ActorIndexViewModel()
            {
                AllActors = entity.Select(this.actorMapper.MapFrom).ToList()
            };
        }
    }
}
