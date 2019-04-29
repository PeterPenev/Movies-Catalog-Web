using MoviesCatalog.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IActorService
    {
        Task<Actor> GetActorAsync(int id);

        Task<IReadOnlyCollection<Actor>> ShowTenActors();

        Task<Actor> CreateActorAsync(string firstName, string lastName, string biography);

        Task<IReadOnlyCollection<Actor>> ShowActorsStartWithSymbolAsync(int id);

        Task<Actor> UpdateActorBiographyAsync(int id, string biography);
    }
}
