using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class ActorIndexViewModel
    {
        public IReadOnlyList<ActorViewModel> Top10Actors { get; set; }
    }
}
