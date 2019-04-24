using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesCatalog.Services
{
    public class ActorService: IActorService
    {
        private readonly MoviesCatalogContext context;

        public ActorService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Actor GetActor(int id)
        {
            var actor = this.context.Actors.Find(id);
            return actor;
        }

        public IReadOnlyCollection<Actor> ShowActorsStartWithSymbol(char symbol)
        {
            var actors = this.context.Actors
                                     .Where(t => t.FirstName.StartsWith(symbol))
                                     .ToList();

            return actors;
        }

        public Actor CreateActor(string firstName, string lastName)
        {
            var actor = this.context.Actors
                                    .FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);

            if (actor != null)
            {
                throw new ArgumentException();
            }

            actor = new Actor() {FirstName = firstName, LastName = lastName};

            this.context.Actors.Add(actor);
            this.context.SaveChanges();
            return actor;
        }
    }
}
