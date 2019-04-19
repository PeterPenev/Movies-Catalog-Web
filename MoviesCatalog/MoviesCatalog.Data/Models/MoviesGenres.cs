using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Data.Models
{
    public class MoviesGenres
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
