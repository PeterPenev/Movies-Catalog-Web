using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Mappers
{
    public class ActorViewModelMapper : IViewModelMapper<Actor, ActorViewModel>
    {
        public ActorViewModel MapFrom(Actor entity)
        {
            return new ActorViewModel()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Biography = entity.Biography
            };
        }
    }
}
