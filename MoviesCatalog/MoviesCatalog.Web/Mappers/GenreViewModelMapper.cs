using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Mappers
{
    public class GenreViewModelMapper : IViewModelMapper<Genre, GenreViewModel>
    {
        public GenreViewModel MapFrom(Genre entity)
        {
            return new GenreViewModel()
            {
                Id= entity.Id,
                Name = entity.Name
            };
        }
    }
}
