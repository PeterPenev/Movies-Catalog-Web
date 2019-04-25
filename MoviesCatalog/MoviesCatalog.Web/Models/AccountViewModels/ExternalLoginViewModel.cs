using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
