using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Data.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public string Picture { get; set; }

        public string Biography { get; set; }

        public ICollection<MoviesActors> ActorMovies { get; set; }
    }
}
