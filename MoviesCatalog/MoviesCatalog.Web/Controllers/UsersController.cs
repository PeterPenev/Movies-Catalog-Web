using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Mappers.Contracts;
using MoviesCatalog.Web.Models;
using MoviesCatalog.Web.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Web.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserService userService;
        private readonly IViewModelMapper<ApplicationUser, UserViewModel> userMapper;
        private readonly IImageOptimizer optimizer;
        private readonly IViewModelMapper<Review, ReviewViewModel> reviewMapper;

        public UsersController(IUserService userService,
                               IViewModelMapper<ApplicationUser, UserViewModel> userMapper,
                               IImageOptimizer optimizer,
                               IViewModelMapper<Review, ReviewViewModel> reviewMapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
            this.optimizer = optimizer ?? throw new ArgumentNullException(nameof(optimizer));
            this.reviewMapper = reviewMapper ?? throw new ArgumentNullException(nameof(reviewMapper));
        }

        [TempData] public string StatusMessage { get; set; }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.ShowAllUsersAsync();

            var userViewModel = users.Select(this.userMapper.MapFrom).ToList();
            return View(userViewModel);
        }

        public async Task<IActionResult> UsersByName(string id)
        {
            var users = await this.userService.ShowUsersStartWithSymbolAsync(id);

            var userViewModel = users.Select(this.userMapper.MapFrom).ToList();
            return View(userViewModel);
        }
    }
}