using MoviesCatalog.Data.Models;

namespace MoviesCatalog.Services.Contracts
{
    public interface IActorService
    {
        Actor GetActor(int id);

        Actor CreateActor(string firstName, string lastName);
    }
}
