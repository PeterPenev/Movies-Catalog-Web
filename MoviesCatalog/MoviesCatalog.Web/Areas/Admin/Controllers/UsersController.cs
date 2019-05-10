using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;

namespace MoviesCatalog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService userServie;
        private readonly IServiceProvider serviceProvider;

        public UsersController(IUserService userServie,
                               IServiceProvider serviceProvider)
        {
            this.userServie = userServie ?? throw new ArgumentNullException(nameof(userServie));
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> Promote(string id)
        {
            var user = await userServie.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userManeger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManeger.IsInRoleAsync(user,"Admin"))
            {
                StatusMessage = $"User \"{user.UserName}\" is already in role \"Admin\".";
                return RedirectToAction("Index", "Users");
            }

            await  userServie.AddRoleAsync(user);
            StatusMessage = $"Successfully promote \"{user.UserName}\" to role \"Admin\".";
            return RedirectToAction("Index", "Users");
        }
    }
}