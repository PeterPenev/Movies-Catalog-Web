﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesCatalog.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<MoviesGenres> MoviesGenres { get; set; }
    }
}
