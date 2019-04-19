using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Trailer { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int NumberOfVotes { get; set; }

        public int TotalRating { get; set; }

        public double AverageRating { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<MoviesGenres> MoviesGenres { get; set; }

        public ICollection<MoviesActors> MoviesActors { get; set; }
    }
}
