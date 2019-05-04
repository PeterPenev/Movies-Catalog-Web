﻿using MoviesCatalog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesCatalog.Services.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string id);

        Task<ApplicationUser> EditUserProfileAsync(string id, string avatar);

        Task<IReadOnlyCollection<ApplicationUser>> ShowUsersStartWithSymbolAsync(string symbol);

        Task<IReadOnlyCollection<ApplicationUser>> ShowAllUsers();

        Task<ICollection<Review>> ShowUserLastFiveReviewsAsync(string userId);

        Task<ICollection<Review>> ShowUserReviewsAsync(string userId);
    }
}
