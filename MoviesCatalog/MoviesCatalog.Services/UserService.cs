using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services.Contracts;
using System;

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
    }
}
