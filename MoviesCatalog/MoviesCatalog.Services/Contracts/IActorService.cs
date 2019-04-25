using MoviesCatalog.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoviesCatalog.Services.Contracts
{
    public interface IActorService
    {
        Actor GetActor(int id);

        Actor CreateActor(string firstName, string lastName);

        IReadOnlyCollection<Actor> ShowActorsStartWithSymbol(char symbol);
    }
}
