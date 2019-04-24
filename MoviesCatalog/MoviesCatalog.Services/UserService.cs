using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;
using System.Linq;

namespace MoviesCatalog.Services
{
    public class UserService : IUserService
    {
        private readonly MoviesCatalogContext context;

        public UserService(MoviesCatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ApplicationUser GetUser(int id)
        {
            var user = this.context.Users.Find(id);
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
    }
}
