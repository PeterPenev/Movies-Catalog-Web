﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var showAllUsers = await this.userService.ShowAllUsers();
            var userIndexView = new UserIndexViewModel()
            {
                AllUsers = showAllUsers.Select(this.userMapper.MapFrom).ToList()
            };
            return View(userIndexView);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.userService.GetUserAsync(id);
            var userId = this.User.GetId();

            var userLastFiveReviews = await this.userService.ShowUserLastFiveReviewsAsync(user.Id);
            var userViewModel = this.userMapper.MapFrom(user);
            userViewModel.CanUserEdit = id == userId;
            userViewModel.LastFiveReviewsByUser = userLastFiveReviews.Select(this.reviewMapper.MapFrom).ToList();

            return View(userViewModel);
        }
      
        public async Task<IActionResult> UsersByName(string id)
        {
            var usersByStartingSymbol = await this.userService.ShowUsersStartWithSymbolAsync(id);

            var userIndexView = new UserIndexViewModel()
            {
                UsersByName = usersByStartingSymbol.Select(this.userMapper.MapFrom).ToList(),
            };

            return View(userIndexView);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userService.GetUserAsync(id);
            var userViewModel = this.userMapper.MapFrom(user);
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
                var actor = await this.userService
                                .EditUserProfileAsync(model.Id, model.Avatar);
                return RedirectToAction("Details", "Users", new { id = actor.Id });
            }

            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }
    }
}