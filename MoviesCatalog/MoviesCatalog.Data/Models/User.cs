using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
