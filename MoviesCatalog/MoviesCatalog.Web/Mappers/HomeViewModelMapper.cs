using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Mappers
{
    public class ActorIndexViewModelMapper : IViewModelMapper<List<IReadOnlyCollection<Movie>>, HomeViewModel>
    {
        private readonly IViewModelMapper<Movie, MovieViewModel> movieMapper;

        public ActorIndexViewModelMapper(IViewModelMapper<Movie, MovieViewModel> movieMapper)
        {
            this.movieMapper = movieMapper ?? throw new ArgumentNullException(nameof(movieMapper));
        }

        public HomeViewModel MapFrom(List<IReadOnlyCollection<Movie>> entity)
        {
            return new HomeViewModel()
            {
                TopTenMoviesByRating = entity[0].Select(this.movieMapper.MapFrom).ToList(),
                TopTenMoviesByReleaseDate = entity[1].Select(this.movieMapper.MapFrom).ToList()
            };
        }
    }
}
