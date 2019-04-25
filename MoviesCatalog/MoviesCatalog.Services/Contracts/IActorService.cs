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

        Task<Actor> CreateActorAsync(string firstName, string lastName);

        Task<IReadOnlyCollection<Actor>> ShowActorsStartWithSymbolAsync(char symbol);
    }
}
