using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;

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

        public IActionResult Details(int id)
        {
            var user = this.userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(this.userMapper.MapFrom(user));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserProfileViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = this.userService
                                .CreateUser(model.UserName, model.Password, model.Email);


                return RedirectToAction(nameof(Details), new { id = user.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }
    }
}