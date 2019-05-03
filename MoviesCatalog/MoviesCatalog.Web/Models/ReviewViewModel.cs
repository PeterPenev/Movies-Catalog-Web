using MoviesCatalog.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Web.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        [MaxLength(500)]
        [Required]
        public string Description { get; set; }

        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public string CanUserEdit { get; set; }

        public string UserName { get; set; }

        public int MovieId { get; set; }

        public string MoviePoster { get; set; }

        public string MovieTitle { get; set; }
    }
}
