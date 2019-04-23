﻿using MoviesCatalog.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Web.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
