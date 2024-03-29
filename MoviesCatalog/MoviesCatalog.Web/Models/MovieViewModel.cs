﻿using Microsoft.AspNetCore.Http;
using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Web.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Trailer { get; set; }

        public IFormFile PosterImage { get; set; }

        public string Poster { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int NumberOfVotes { get; set; }
        
        public double AverageRating { get; set; }

        public IFormFile SliderPoster { get; set; }

        public string SliderImage { get; set; }

        public IReadOnlyCollection<GenreViewModel> AllGenresByMovie { get; set; }

        public IReadOnlyCollection<ActorViewModel> AllActorsByMovie { get; set; }

        public IReadOnlyCollection<ReviewViewModel> LastFiveReviewsByMovie { get; set; }

        public IReadOnlyCollection<ReviewViewModel> AllReviewsByMovie { get; set; }

        public override string ToString() => $"{this.AverageRating:F1}";
    }
}
