using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesCatalog.Data.Models;
using System;

namespace MoviesCatalog.Data.EntityConfiguration
{
    public class MoviesGenresConfig : IEntityTypeConfiguration<MoviesGenres>
    {
        public void Configure(EntityTypeBuilder<MoviesGenres> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.GenreId });
        }
    }
}
