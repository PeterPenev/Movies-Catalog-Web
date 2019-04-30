using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Web.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Trailer { get; set; }

        //[Required]
        public string Poster { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        public string Description { get; set; }

        //[Required]
        public string User { get; set; }

        //[Required]
        public int NumberOfVotes { get; set; }

        //[Required]
        public double AverageRating { get; set; }

        //[Required]
        public ICollection<Genre> Genres { get; set; }

        public override string ToString() => $"{this.Title} {this.Description} {this.ReleaseDate.ToShortDateString()}";
    }
}
