using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.ReviewServiceTests
{
    [TestClass]
    public class AddReviewAsync_Should
    {
        [TestMethod]
        public async Task Succeed_WhenUserNotVoted()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_WhenUserNotVoted));

            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Users.AddAsync(TestHelper.TestUser2());
                await arrangeContext.Movies.AddAsync(TestHelper.TestMovie1());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var review = await sut.AddReviewToMovie(1, TestHelper.TestUser2().Id, "Perfect movie.", 5);
                var movie = review.Movie;
                Assert.IsTrue(movie.TotalRating == 10);
                Assert.IsTrue(movie.AverageRating == 5);
                Assert.IsTrue(movie.NumberOfVotes == 2);
                Assert.IsTrue(assertContext.Reviews.Count() == 1);
            }
        }

        [TestMethod]
        public async Task SetCorrect_WhenReviewIsNotActive()
        {
            var options = TestUtils.GetOptions(nameof(SetCorrect_WhenReviewIsNotActive));

            using (var arrangeContext = new MoviesCatalogContext(options))
            {
               await arrangeContext.Users.AddAsync(TestHelper.TestUser1());
               await  arrangeContext.Reviews.AddAsync(TestHelper.TestReview2());

               await arrangeContext.Movies.AddAsync(TestHelper.TestMovie1());
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var review = await sut.AddReviewToMovie(TestHelper.TestMovie1().Id, TestHelper.TestUser1().Id, "Perfect movie!", 7);

                Assert.AreEqual("Perfect movie!", review.Description);
                Assert.AreEqual(7, review.Rating);
                Assert.IsTrue(!review.IsDeleted);
            }
        }

        [TestMethod]
        public async Task AddReviewToMovie_ReturnCorrectReview_WhenOverrideExistingNonActive()
        {
            var options = TestUtils.GetOptions(nameof(AddReviewToMovie_ReturnCorrectReview_WhenOverrideExistingNonActive));

            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                arrangeContext.Users.Add(TestHelper.TestUser1());
                arrangeContext.Reviews.Add(TestHelper.TestReview3());
                arrangeContext.Movies.Add(TestHelper.TestMovie2());
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var rating = await sut.AddReviewToMovie(1, TestHelper.TestUser1().Id, "Perfect movie!", 3);
                var movie = rating.Movie;

                Assert.AreEqual(8, movie.TotalRating);
                Assert.IsTrue(movie.AverageRating == 4);
                Assert.IsTrue(movie.NumberOfVotes == 2);
                Assert.IsTrue(assertContext.Reviews.Count() == 1);
            }
        }
    }
}
