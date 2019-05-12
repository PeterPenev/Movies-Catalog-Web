using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ICollection<Review> Reviews { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
