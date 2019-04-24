using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Web.Models
{
    public class ActorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "The First Name can contain only letters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "The Last Name can contain only letters")]
        public string LastName { get; set; }

        public string Biography { get; set; }

        public ICollection<MovieViewModel> Movies { get; set; }
    }
}
