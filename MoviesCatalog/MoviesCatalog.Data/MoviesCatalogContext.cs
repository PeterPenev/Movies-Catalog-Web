using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data.EntityConfiguration;
using MoviesCatalog.Data.Models;

namespace MoviesCatalog.Data
{
    public class MoviesCatalogContext : DbContext
    {
        public MoviesCatalogContext(DbContextOptions options)
               : base(options)
        {
        }

        public MoviesCatalogContext()
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MoviesGenresConfig());
            modelBuilder.ApplyConfiguration(new MoviesActorsConfig());
            modelBuilder.ApplyConfiguration(new MovieConfig());
        }
    }
}
