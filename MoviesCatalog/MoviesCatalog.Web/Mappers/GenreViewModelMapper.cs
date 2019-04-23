using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Mappers
{
    public class GenreViewModelMapper : IViewModelMapper<Genre, GenreViewModel>
    {
        public GenreViewModel MapFrom(Genre entity)
        {
            return new GenreViewModel()
            {
                Name = entity.Name,

            }
        }
    }
}
