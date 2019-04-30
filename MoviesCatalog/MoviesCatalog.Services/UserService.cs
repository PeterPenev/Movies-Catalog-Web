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

        public Task<ApplicationUser> GetUserAsync(string id)
        {
            var user = this.context.Users.FindAsync(id);
            return user;
        }

        public ApplicationUser CreateUser(string userName, string password, string email)
        {
            var user = context.Users
                              .FirstOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                user = new ApplicationUser()
                           { UserName = userName, PasswordHash = password, Email = email};
            }

            else if (user.IsDeleted)
            {
                user.IsDeleted = false;
                context.SaveChanges();
                return user;
            }
            else
            {
                throw new ArgumentException();
            }
            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> ShowUsersStartWithSymbolAsync(int id)
        {
            var symbol = (char)id;
            var users = await this.context.Users
                                     .Where(t => t.UserName.ToLower().StartsWith(symbol.ToString().ToLower()))
                                     .OrderBy(x => x.UserName)
                                     .ToListAsync();

            return users;
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> ShowTenUsers()
        {
            var users = await this.context.Users
                                     .Take(10)
                                     .OrderBy(x => x.UserName)
                                     .ToListAsync();

            return users;
        }
    }
}
