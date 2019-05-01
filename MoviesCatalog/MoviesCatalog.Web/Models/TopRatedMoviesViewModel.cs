using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class TopRatedMoviesViewModel
    {
        public IReadOnlyCollection<MovieViewModel> TopRatedMovies { get; set; }
    }
}
