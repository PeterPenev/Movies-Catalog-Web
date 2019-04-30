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

        [Required]
        public string Biography { get; set; }

        public string Picture { get; set; }

        public bool CanUserEdit { get; set; }

        public ICollection<MovieViewModel> Movies { get; set; }

        public ICollection<ActorViewModel> ShowAllActors { get; set; }
    }
}
