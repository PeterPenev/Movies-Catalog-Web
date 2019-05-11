using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class SearchMovieViewModel
    {
        [Required]        
        public string SearchName { get; set; }

        public IReadOnlyList<MovieViewModel> SearchResults { get; set; } = new List<MovieViewModel>();
    }
}
