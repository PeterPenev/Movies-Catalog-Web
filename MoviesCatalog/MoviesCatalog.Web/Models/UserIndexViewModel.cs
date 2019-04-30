using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class UserIndexViewModel
    {
        public IReadOnlyCollection<UserViewModel> Top10Users { get; set; }

        public IReadOnlyCollection<UserViewModel> UsersByName { get; set; }
    }
}
