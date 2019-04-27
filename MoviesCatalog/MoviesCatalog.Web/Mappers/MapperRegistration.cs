using Microsoft.Extensions.DependencyInjection;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System.Collections.Generic;

namespace MoviesCatalog.Web.Mappers
{
    public static class MapperRegistration
    {
        public static IServiceCollection AddCustomMappers(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelMapper<Movie, MovieViewModel>, MovieViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Review, ReviewViewModel>, ReviewViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Actor, ActorViewModel>, ActorViewModelMapper>();
            services.AddSingleton<IViewModelMapper<Genre, GenreViewModel>, GenreViewModelMapper>();
            //services.AddSingleton<IViewModelMapper<Movie, HomeViewModel>, HomeViewModelMapper>();
           
            return services;
        }
    }
}
