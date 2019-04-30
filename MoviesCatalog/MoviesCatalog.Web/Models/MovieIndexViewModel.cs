using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class MovieIndexViewModel
    {
        public IReadOnlyCollection<MovieViewModel> Latest10Movies { get; set; } 

        public IReadOnlyCollection<MovieViewModel> MoviesByName { get; set; }
    }
}
