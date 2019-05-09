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

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            var actor =  await this.context.Actors.FindAsync(id);

            if (actor == null)
            {
                throw new ArgumentException();
            }

            return actor;
        }

        public async Task<IReadOnlyCollection<Actor>> ShowActorsStartWithSymbolAsync(char symbol)
        {
            var actors = await this.context.Actors
                                     .Where(t => t.FirstName.ToLower().StartsWith(symbol.ToString().ToLower()) ||
                                                  t.LastName.ToLower().StartsWith(symbol.ToString().ToLower()))
                                     .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                                     .ToListAsync();

            return actors;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowActorMoviesAsync(int actorId)
        {
            var movies = await this.context.MoviesActors
                             .Where(a => a.ActorId == actorId)
                             .Select(m => m.Movie)
                             .ToListAsync();

            return movies;
        }

        public async Task<IReadOnlyCollection<Actor>> ShowAllActorsAsync()
        {
            var actors = await this.context.Actors
                                           .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                                           .ToListAsync();

            return actors;
        }

        public async Task<IReadOnlyCollection<Movie>> ShowLastFiveActorMoviesAsync(int actorId)
        {
            var movies = await this.context.MoviesActors
                             .Where(a => a.ActorId == actorId)
                             .Select(m => m.Movie)
                             .Take(5)
                             .OrderByDescending(x => x.ReleaseDate)
                             .ToListAsync();

            return movies;
        }

        public async Task<Actor> FindActorByNameAsync(string firstName, string lastName)
        {
            return await this.context.Actors
                                    .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);
        }

        public async Task<Movie> AddActorToMovieAsync(int movieId, int actorId)
        {
            var movie = await this.context.Movies.Include(m => m.MoviesActors).FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie.MoviesActors.Any(a => a.ActorId == actorId))
            {
                throw new ArgumentException();
            }
                
            await this.context.MoviesActors.AddAsync(new MoviesActors() { MovieId = movie.Id, ActorId = actorId });

            await this.context.SaveChangesAsync();

            return movie;
        }

        public async Task<Actor> CreateActorAsync(string firstName, string lastName, string biography)
        {
            var actor =  new Actor() {FirstName = firstName, LastName = lastName, Biography = biography};

            await this.context.Actors.AddAsync(actor);
            await this.context.SaveChangesAsync();
            return actor;
        }

        public async Task<Actor> UpdateActorAsync(Actor actor, string picture, string biography)

        {
            actor.Biography = biography;
            actor.Picture = picture;
            await this.context.SaveChangesAsync();
            return actor;
        }
    }
}
