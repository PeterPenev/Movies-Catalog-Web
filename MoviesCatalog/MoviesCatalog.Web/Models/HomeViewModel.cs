﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class HomeViewModel
    {
        public IReadOnlyList<MovieViewModel> Teams { get; set; }
    }
}
