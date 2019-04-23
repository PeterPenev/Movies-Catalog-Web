using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<MovieViewModel> MoviesGenres { get; set; }
    }
}
