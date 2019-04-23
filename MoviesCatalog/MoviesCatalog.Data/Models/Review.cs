using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int Rating { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
