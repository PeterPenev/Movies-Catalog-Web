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

            if (actor == null)
            {
                throw new ArgumentException();
            }

            return actor;
        }

        public async Task<IReadOnlyCollection<Actor>> ShowActorsStartWithSymbolAsync(string symbol)
        {
            var actors = await this.context.Actors
                                     .Where(t => t.FirstName.ToLower().StartsWith(symbol.ToString().ToLower()) ||
                                                  t.LastName.ToLower().StartsWith(symbol.ToString().ToLower()))
                                     .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                                     .ToListAsync();

            return actors;
        }

        public async Task<IReadOnlyCollection<Actor>> ShowAllActors()
        {
            var actors = await this.context.Actors
                                           .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                                           .ToListAsync();

            return actors;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowMoviesByActor(Actor actor)
        {
            var movies = await this.context.MoviesActors
                             .Where(a => a.ActorId == actor.Id)
                             .Select(m => m.Movie)
                             .ToListAsync();

            return movies;
        }

        public async Task<Actor> CreateActorAsync(string firstName, string lastName, string biography)
        {
            var actor = await this.context.Actors
                                    .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);
            if (actor != null)
            {
                throw new ArgumentException();
            }

            actor =  new Actor() {FirstName = firstName, LastName = lastName, Biography = biography};

            this.context.Actors.Add(actor);
            await this.context.SaveChangesAsync();
            return actor;
        }

        public async Task<Actor> UpdateActorBiographyAsync(int id, string biography)
        {
            var actor = await this.context.Actors
                                    .FindAsync(id);
            if (actor == null)
            {
                throw new ArgumentException();
            }

            actor.Biography = biography;
            await this.context.SaveChangesAsync();
            return actor;
        }
    }
}
