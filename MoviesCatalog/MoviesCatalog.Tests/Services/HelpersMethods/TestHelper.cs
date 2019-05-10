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
                Id = 1,
                User = TestUser1(),
                Movie = TestMovie1(),
                Rating = 4,
                Description = "Very nice movie",
                IsDeleted = false,
                UserId = "24776b40-e657-479a-846c-00da0e80b7c5"

            };
        }

        public static Review TestReview2()
        {
            return new Review
            {
                Id = 2,
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
                Id = 3,
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
                Id = "24776b40-e657-479a-846c-00da0e80b7c5",
                
            };
        }

        public static ApplicationUser TestUser2()
        {
            return new ApplicationUser
            {
                 UserName = "Ivan",
                 Email = "di@abv.bg",
                 PasswordHash = "Admin123@",
                 Id = "01418677-E407-4C7B-9D84-0BA79C9BFCBD"               
            };
        }

        public static Movie TestMovie1()
        {
            return new Movie
            {
                Title = "Gone in 60 seconds",
                Id = 1,
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 1,
                TotalRating = 5,
                User = TestUser1()
            };
        }

        public static Movie TestMovie3()
        {
            return new Movie
            {
                Title = "Game of thrones",
                Id = 3,
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 2,
                TotalRating = 10,
                User = TestUser2()
            };
        }


        public static Movie TestMovie2()
        {
            return new Movie
            {
                Title = "Gladiator",
                Id = 2,
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 2,
                TotalRating = 10,
                User = TestUser2()
            };
        }


        public static Movie TestMovie4()
        {
            return new Movie
            {
                Title = "End game",
                Id = 4,
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 2,
                TotalRating = 10,
                User = TestUser2()
            };
        }

        public static Movie TestMovie5()
        {
            return new Movie
            {
                Title = "The one",
                Id = 5,
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 2,
                TotalRating = 10,
                User = TestUser2()
            };
        }

        public static Movie TestMovie6()
        {
            return new Movie
            {
                Title = "Test movie",
                Id = 6,
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 2,
                TotalRating = 10,
                User = TestUser2()
            };
        }

        public static Movie TestMovie7()
        {
            return new Movie
            {
                Title = "Test movie7",
                Id = 7,
                ReleaseDate = new DateTime(01 / 01 / 2017),
                NumberOfVotes = 2,
                TotalRating = 10,
                User = TestUser2()
            };
        }

        public static MoviesActors MoviesActors4()
        {
            return new MoviesActors
            {
                ActorId = 1,
                MovieId = 3,
                Actor = TestActor1(),
                Movie = TestMovie3()
            };
        }



        public static Actor TestActor1()
        {
            return new Actor
            {
                Id = 1,
                FirstName = "Brad",
                LastName = "Pit",
                Biography = "Very good actor",
            };
        }

        public static Actor TestActor2()
        {
            return new Actor
            {
                Id = 2,
                FirstName = "John",
                LastName = "Dow",
                Biography = "Very good actor"
            };
        }

        public static Actor TestActor3()
        {
            return new Actor
            {
                Id = 3,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Biography = "Very good actor"
            };
        }

        public static Actor TestActor4()
        {
            return new Actor
            {
                Id = 4,
                FirstName = "Ivo",
                LastName = "Ivanov",
                Biography = "Very good actor"
            };
        }

        public static MoviesActors MoviesActors1()
        {
            return new MoviesActors
            {
                ActorId = 1,
                MovieId = 1,
                Actor = TestActor1(),
                Movie = TestMovie1()
            };
        }

        public static MoviesActors MoviesActors2()
        {
            return new MoviesActors
            {
                ActorId = 1,
                MovieId = 2,
                Actor = TestActor1(),
                Movie = TestMovie2()
            };
        }

        public static MoviesActors MoviesActors3()
        {
            return new MoviesActors
            {
                ActorId = 2,
                MovieId = 2,
                Actor = TestActor2(),
                Movie = TestMovie2()
            };
        }

        public static Genre TestGenre1()
        {
            return new Genre
            {
                Id = 1,
                Name = "Drama"
            };
        }

        public static Genre TestGenre2()
        {
            return new Genre
            {
                Id = 2,
                Name = "Action"
            };
        }

        public static MoviesGenres MoviesGenres1()
        {
            return new MoviesGenres
            {
                GenreId = 1,
                MovieId = 100,
                Genre = TestGenre1(),
                Movie = TestMovie100()
            };
        }

        public static MoviesGenres MoviesGenres2()
        {
            return new MoviesGenres
            {
                GenreId = 1,
                MovieId = 200,
                Genre = TestGenre1(),
                Movie = TestMovie200()
            };
        }

        public static MoviesGenres MoviesGenres3()
        {
            return new MoviesGenres
            {
                GenreId = 2,
                MovieId = 100,
                Genre = TestGenre2(),
                Movie = TestMovie100()
            };
        }

        public static Movie TestMovie100()
        {
            return new Movie
            {
                Title = "Movie 100",
                Id = 100,
                ReleaseDate = new DateTime(09 / 09 / 2016),
                NumberOfVotes = 1,
                TotalRating = 5,
                User = TestUser1()
            };
        }

        public static Movie TestMovie200()
        {
            return new Movie
            {
                Title = "Movie 200",
                Id = 200,
                ReleaseDate = new DateTime(01 / 01 / 2016),
                NumberOfVotes = 1,
                TotalRating = 2,
                User = TestUser2()
            };
        }

        public static Movie TestMovie300()
        {
            return new Movie
            {
                Title = "Movie 300",
                Id = 300,
                ReleaseDate = new DateTime(09 / 09 / 2015),
                NumberOfVotes = 1,
                TotalRating = 5,
                User = TestUser1()
            };
        }


        public static MoviesActors TestMoviesActors100()
        {
            return new MoviesActors
            {
                ActorId = 100,
                MovieId = 100,
                Actor = TestActor100(),
                Movie = TestMovie100()
            };
        }

        public static MoviesActors TestMoviesActors200()
        {
            return new MoviesActors
            {
                ActorId = 200,
                MovieId = 100,
                Actor = TestActor200(),
                Movie = TestMovie100()
            };
        }

        public static Actor TestActor100()
        {
            return new Actor
            {
                Id = 100,
                FirstName = "Brad100",
                LastName = "Pit100",
                Biography = "Very good actor"
            };
        }

        public static Actor TestActor200()
        {
            return new Actor
            {
                Id = 200,
                FirstName = "Brad200",
                LastName = "Pit200",
                Biography = "Very good actor"
            };
        }
>>>>>>> e6d29baf25c234132e06d589225a8e5cfb8737d7
    }
}
