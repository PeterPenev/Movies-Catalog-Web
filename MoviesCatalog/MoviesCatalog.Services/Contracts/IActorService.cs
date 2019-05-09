using MoviesCatalog.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IActorService
    {
        Task<Actor> GetActorByIdAsync(int id);

        Task<IReadOnlyCollection<Actor>> ShowAllActorsAsync();

        Task<Actor> CreateActorAsync(string firstName, string lastName, string biography);

        Task<IReadOnlyCollection<Actor>> ShowActorsStartWithSymbolAsync(string id);

        Task<Actor> UpdateActorAsync(Actor actor, string picture, string biography);


        Task<IReadOnlyCollection<Movie>> ShowLastFiveActorMoviesAsync(int actorId);

        Task<bool> IsActorExistAsync(string firstName, string lastName);

        Task<Movie> AddActorToMovieAsync(int movieId, int actorId);
    }
}
