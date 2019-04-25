using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Services
{
    public class ActorService: IActorService
    {
        private readonly MoviesCatalogContext context;

        public ActorService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<Actor> GetActorAsync(int id)
        {
            var actor = this.context.Actors.FindAsync(id);
            return actor;
        }

        public async Task<IReadOnlyCollection<Actor>> ShowActorsStartWithSymbolAsync(char symbol)
        {
            var actors = await this.context.Actors
                                     .Where(t => t.FirstName.StartsWith(symbol))
                                     .ToListAsync();

            return actors;
        }

        public async Task<IReadOnlyCollection<Actor>> ShowTenActors()
        {
            var actors = await this.context.Actors
                                     .Take(10)
                                     .ToListAsync();

            return actors;
        }

        public async Task<Actor> CreateActorAsync(string firstName, string lastName)
        {
            var actor = await this.context.Actors
                                    .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);

            if (actor != null)
            {
                throw new ArgumentException();
            }

            actor =  new Actor() {FirstName = firstName, LastName = lastName};

            this.context.Actors.Add(actor);
            await this.context.SaveChangesAsync();
            return actor;
        }
    }
}
