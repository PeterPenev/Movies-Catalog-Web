using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using MoviesCatalog.Web.Extensions;
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
        private readonly IViewModelMapper<Review, ReviewViewModel> reviewMapper;

        public UsersController(IUserService userService,
                               IViewModelMapper<ApplicationUser, UserViewModel> userMapper,
                               IViewModelMapper<Review, ReviewViewModel> reviewMapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
            this.reviewMapper = reviewMapper ?? throw new ArgumentNullException(nameof(reviewMapper));
        }

        [TempData] public string StatusMessage { get; set; }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.ShowAllUsersAsync();

            var userViewModel = users.Select(this.userMapper.MapFrom).ToList();
            return View(userViewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.userService.GetUserByIdAsync(id);
            var userId = this.User.GetId();

            var userLastFiveReviews = await this.userService.ShowUserLastFiveReviewsAsync(user.Id);
            var userViewModel = this.userMapper.MapFrom(user);
            userViewModel.CanUserEdit = id == userId;
            userViewModel.LastFiveReviewsByUser = userLastFiveReviews.Select(this.reviewMapper.MapFrom).ToList();

            return View(userViewModel);
        }
      
        public async Task<IActionResult> UsersByName(string id)
        {
            var users = await this.userService.ShowUsersStartWithSymbolAsync(id);

            var userViewModel = users.Select(this.userMapper.MapFrom).ToList();
            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var userId = this.User.GetId();
            var user = await this.userService.GetUserByIdAsync(id);
            var userViewModel = this.userMapper.MapFrom(user);
            userViewModel.CanUserEdit = id == userId;
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await this.userService.GetUserByIdAsync(model.Id);
                if (user == null)
                {
                    RedirectToAction("Index", "Reviews");
                }
                var actor = await this.userService
                                .EditUserProfileAsync(user, model.Avatar);
                return RedirectToAction("Details", "Users", new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                RedirectToAction("Index", "Movies");
            }
            var userId = this.User.GetId();
            var userViewModel = this.userMapper.MapFrom(user);
            userViewModel.CanUserEdit = userId == id;
           
            return View(userViewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await userService.DeleteUserAsync(id);
            StatusMessage = "Successfully deleted your profile.";
            return RedirectToAction("Logout", "Account");
        }
    }
}