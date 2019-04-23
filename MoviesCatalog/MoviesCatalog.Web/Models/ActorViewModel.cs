using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Web.Models
{
    public class ActorViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public string Biography { get; set; }

        public ICollection<MovieViewModel> Movies { get; set; }
    }
}
