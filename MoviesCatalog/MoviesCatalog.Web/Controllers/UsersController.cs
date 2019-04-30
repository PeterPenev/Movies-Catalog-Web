using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Controllers
{
    public class UsersController : Controller
    {
    
        private readonly IUserService userService;
        private readonly IViewModelMapper<ApplicationUser, UserViewModel> userMapper;

        public UsersController(IUserService userService,
                               IViewModelMapper<ApplicationUser, UserViewModel> userMapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
        }

        public async Task<IActionResult> Index()
        {
            var showTo10Users = await this.userService.ShowTenUsers();

            var userIndexView = new UserIndexViewModel()
            {
                Top10Users = showTo10Users.Select(this.userMapper.MapFrom).ToList()
            };
            return View(userIndexView);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(this.userMapper.MapFrom(user));
        }
      
        public async Task<IActionResult> UsersByName(int id)
        {
            var usersByStartingSymbol = await this.userService.ShowUsersStartWithSymbolAsync(id);

            var userIndexView = new UserIndexViewModel()
            {
                UsersByName = usersByStartingSymbol.Select(this.userMapper.MapFrom).ToList()
            };

            return View(userIndexView);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel model)
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