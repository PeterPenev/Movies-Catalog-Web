using MoviesCatalog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalog.Tests.Services.HelpersMethods
{
    public static class TestHelper
    {
        public static Review TestReview1()
        {
            return new Review
            {
                User = TestUser1(),
                Movie = TestMovie1(),
                Rating = 4,
                Description = "Very nice movie",
                IsDeleted = false
            };
        }

        public static Review TestReview2()
        {
            return new Review
            {
                User = TestUser2(),
                Movie = TestMovie1(),
                Rating = 4,
                Description = "Very nice movie",
                IsDeleted = true
            };
        }

        public static Review TestReview3()
        {
            return new Review
            {
                User = TestUser1(),
                Movie = TestMovie2(),
                Rating = 5,
                Description = "Very nice movie",
                IsDeleted = true
            };
        }


        public static ApplicationUser TestUser1()
        {
            return new ApplicationUser
            {
                UserName = "Pitur",
                Email = "d@abv.bg",
                PasswordHash = "Admin123@",
                //Id = "00000000 - 0000 - 0000 - 0000 - 000000000001",
                
            };
        }

        public static ApplicationUser TestUser2()
        {
            return new ApplicationUser

            {    UserName = "Ivan",
                 Email = "di@abv.bg",
                 PasswordHash = "Admin123@",
                //Id = "00000000 - 0000 - 0000 - 0000 - 000000000002",
                
            };
        }

        public static Movie TestMovie1()
        {
            return new Movie
            {
                Title = "Gone in 60 seconds",
                
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 1,
                TotalRating = 5
            };
        }

        public static Movie TestMovie2()
        {
            return new Movie
            {
                Title = "Gladiator",
               
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 2,
                TotalRating = 10
            };
        }

        public static Actor TestActor1()
        {
            return new Actor
            {
                
                FirstName = "Brad",
                LastName = "Pit"
            };
        }
    }
}
