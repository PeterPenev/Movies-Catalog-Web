﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IReadOnlyCollection<Movie>> ShowLastFiveActorMovies(int actorId)
        {
            var movies = await this.context.MoviesActors
                             .Where(a => a.ActorId == actorId)
                             .Select(m => m.Movie)
                             .Take(5)
                             .OrderByDescending(x => x.ReleaseDate)
                             .ToListAsync();

            return movies;
        }

        public async Task<bool> IsActorExistAsync(string firstName, string lastName)
        {
            return await this.context.Actors
                                    .AnyAsync(x => x.FirstName == firstName && x.LastName == lastName);
        }

        public Movie AddActorToMovie(int movieId, int actorId)
        {
            var movie = this.context.Movies.Include(m => m.MoviesActors).FirstOrDefault(m => m.Id == movieId);
            if (movie.MoviesActors.Any(a => a.ActorId == actorId))
                throw new ArgumentException();

            this.context.MoviesActors.Add(new MoviesActors() { MovieId = movie.Id, ActorId = actorId });

            this.context.SaveChanges();

            return movie;
        }



        public async Task<Actor> CreateActorAsync(string firstName, string lastName, string biography)
        {
            var actor =  new Actor() {FirstName = firstName, LastName = lastName, Biography = biography};

            this.context.Actors.Add(actor);
            await this.context.SaveChangesAsync();
            return actor;
        }

        public async Task<Actor> UpdateActorAsync(Actor actor,string firstName, 
                                                  string lastName, string picture, string biography)
        {

            actor.Biography = biography;
            await this.context.SaveChangesAsync();
            return actor;
        }
    }
}
