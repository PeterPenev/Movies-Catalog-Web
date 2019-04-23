﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Web.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Trailer { get; set; }

        [Required]
        public string Poster { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string Description { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public int NumberOfVotes { get; set; }

        [Required]
        public double AverageRating { get; set; }
    }
}
