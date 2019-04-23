using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
