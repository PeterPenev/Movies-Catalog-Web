using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCatalog.Services
{
    public class UserService : IUserService
    {
        private readonly MoviesCatalogContext context;
        private readonly IServiceProvider serviceProvider;

        public UserService(MoviesCatalogContext context,
                          IServiceProvider serviceProvider)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = await this.context.Users.Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task AddRoleAsync(ApplicationUser user)
        {
            var userManeger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await userManeger.AddToRoleAsync(user, "Admin");
        }



        public async Task<IReadOnlyCollection<ApplicationUser>> ShowUsersStartWithSymbolAsync(string symbol)
        {
            var users = await this.context.Users.Include(x => x.Reviews)
                                     .Where(t => t.UserName.ToLower().StartsWith(symbol.ToString().ToLower()))
                                     .OrderBy(x => x.UserName)
                                     .ToListAsync();

            return users;
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> ShowAllUsersAsync()
        {
            var users = await this.context.Users.Include(x => x.Reviews)
                                          .OrderBy(x => x.UserName)
                                          .ToListAsync();

            return users;
        }


        public async Task<ICollection<Review>> ShowUserLastFiveReviewsAsync(string userId)
        {

            var reviews = await context.Reviews
                                        .Where(x => x.User.Id == userId)
                                        .Take(5)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Include(x => x.Movie)
                                        .ToListAsync();
            return reviews;
        }

        public async Task<ICollection<Review>> ShowUserReviewsAsync(string userId)
        {
            var reviews = await context.Reviews
                                        .Where(x => x.User.Id == userId)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Include(x => x.Movie)
                                        .ToListAsync();
            return reviews;
        }

    }
}
