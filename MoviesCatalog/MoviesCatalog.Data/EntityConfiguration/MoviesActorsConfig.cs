using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesCatalog.Data.Models;

namespace MoviesCatalog.Data.EntityConfiguration
{
    public class MoviesActorsConfig : IEntityTypeConfiguration<MoviesActors>
    {
        public void Configure(EntityTypeBuilder<MoviesActors> builder)
        {
            builder.HasKey(x => new { x.ActorId, x.MovieId });
        }
    }
}
