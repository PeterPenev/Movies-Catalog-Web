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

        public string Poster { get; set; }

        public string SliderImage { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int NumberOfVotes { get; set; }

        public double TotalRating { get; set; }

        public double AverageRating { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<MoviesGenres> MoviesGenres { get; set; }

        public ICollection<MoviesActors> MoviesActors { get; set; }

    }
}
