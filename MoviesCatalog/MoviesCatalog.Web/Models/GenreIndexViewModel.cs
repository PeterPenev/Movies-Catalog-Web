using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class GenreIndexViewModel
    {
        public IReadOnlyCollection<MovieViewModel> AllGenres { get; set; }

        public IReadOnlyCollection<MovieViewModel> MoviesByGenre { get; set; }
    }
}
