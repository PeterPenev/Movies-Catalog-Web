using MoviesCatalog.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IActorService
    {
        Task<Actor> GetActorAsync(int id);

        Task<IReadOnlyCollection<Actor>> ShowAllActors();

        Task<Actor> CreateActorAsync(string firstName, string lastName, string biography);

        Task<IReadOnlyCollection<Actor>> ShowActorsStartWithSymbolAsync(string id);

        Task<Actor> UpdateActorBiographyAsync(int id, string biography);

        Task<IReadOnlyCollection<Movie>> ShowMoviesByActor(Actor actor);
    }
}
