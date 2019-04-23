using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;

namespace MoviesCatalog.Services
{
    public class ActorService: IActorService
    {
        private readonly MoviesCatalogContext context;

        public ActorService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public Actor GetActor(int id)
        {
            var actor = this.context.Actors.Find(id);
            return actor;
        }

    }
}
