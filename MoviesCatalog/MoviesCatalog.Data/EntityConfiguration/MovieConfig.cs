using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesCatalog.Data.Models;

namespace MoviesCatalog.Data.EntityConfiguration
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasOne(m => m.User)
               .WithMany(u => u.Movies)
               .HasForeignKey(m => m.UserId)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
