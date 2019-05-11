using Microsoft.AspNetCore.Http;
using MoviesCatalog.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Web.Models
{
    public class ActorViewModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "The First Name can contain only letters")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "The Last Name can contain only letters")]
        public string LastName { get; set; }

        public string Biography { get; set; }

        public IFormFile ActorPicture { get; set; }

        public string Picture { get; set; }

        public bool CanUserEdit { get; set; }

        public ICollection<MovieViewModel> LastFiveMoviesByActor { get; set; }
    }
}
