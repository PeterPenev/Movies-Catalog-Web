using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;

namespace MoviesCatalog.Web.Controllers
{
    public class UsersController : Controller
    {
    
        private readonly IUserService userService;
        private readonly IViewModelMapper<ApplicationUser, UserProfileViewModel> userMapper;

        public UsersController(IUserService userService,
                               IViewModelMapper<ApplicationUser, UserProfileViewModel> userMapper)
        {
            this.userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
            this.userMapper = userMapper ?? throw new System.ArgumentNullException(nameof(userMapper));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(UserViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //}
    }
}