using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required, MinLength(6)]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required, Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int NumberOfReviews { get; set; }

        public bool CanUserEdit { get; set; }

        public IReadOnlyCollection<ReviewViewModel> LastFiveReviewsByUser { get; set; }

}
}
