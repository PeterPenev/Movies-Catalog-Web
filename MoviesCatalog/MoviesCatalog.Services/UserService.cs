using Microsoft.EntityFrameworkCore;
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

        public UserService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            var user = await this.context.Users.FindAsync(id);

            if (user == null || user.IsDeleted)
            {
                throw new ArgumentException();
            }

            return user;
        }

        public async Task<ApplicationUser> EditUserProfileAsync(string id, string avatar)
        {
            var user = await this.context.Users
                                    .FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException();
            }

            user.Avatar = avatar;
            await this.context.SaveChangesAsync();
            return user;
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> ShowUsersStartWithSymbolAsync(string symbol)
        {
            var users = await this.context.Users
                                     .Where(t => t.UserName.ToLower().StartsWith(symbol.ToString().ToLower()))
                                     .OrderBy(x => x.UserName)
                                     .ToListAsync();

            return users;
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> ShowAllUsers()
        {
            var users = await this.context.Users
                                          .OrderBy(x => x.UserName)
                                          .ToListAsync();

            return users;
        }

        public async Task<ICollection<Review>> ShowUserLastFiveReviewsAsync(string userId)
        {

            var reviews =  await context.Reviews
                                        .Where(x => x.User.Id == userId && !x.IsDeleted)
                                        .Take(5)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Include(x => x.Movie)
                                        .ToListAsync();
            return reviews;
        }

        public async Task<ICollection<Review>> ShowUserReviewsAsync(string userId)
        {
            var reviews =  await context.Reviews
                                        .Where(x => x.User.Id == userId && !x.IsDeleted)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .Include(x => x.Movie)
                                        .ToListAsync();
            return reviews;
        }
    }
}
