using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Services;
using MoviesCatalog.Services.Utils;
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
    public class EditReview_Should
    {
        [TestMethod]
        public async Task Succeed_WhenReviewIsFromUser()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_WhenReviewIsFromUser));

            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Reviews.AddAsync(TestHelper.TestReview1());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var review = await sut.EditReviewAsync(TestHelper.TestReview1(), TestHelper.TestUser1().Id, 3, "Very nice movie");
                var movie = review.Movie;

                Assert.IsTrue(movie.TotalRating == 4);
                Assert.IsTrue(movie.NumberOfVotes == 1);
                Assert.IsTrue(assertContext.Reviews.Count() == 1);
            }
        }

        [TestMethod]
        public async Task ThrowExeption_WhenReviewNotFromUser()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExeption_WhenReviewNotFromUser));

            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                arrangeContext.Reviews.Add(TestHelper.TestReview1());
                arrangeContext.Users.Add(TestHelper.TestUser2());
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.EditReviewAsync(TestHelper.TestReview1(), TestHelper.TestUser2().Id, 3, "very nice movie" ));
                Assert.AreEqual(ex.Message, string.Format(ServicesConstants.ReviewNotFromUser,
                                                           TestHelper.TestUser2().UserName));

            }
        }

        [TestMethod]
        public async Task ThrowExeption_ReviewIsDeleted()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExeption_ReviewIsDeleted));

            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                arrangeContext.Reviews.Add(TestHelper.TestReview3());
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.EditReviewAsync(TestHelper.TestReview3(), TestHelper.TestUser2().Id, 3, "very nice movie"));
                Assert.AreEqual(ex.Message, string.Format(ServicesConstants.ReviewNotPresent));

            }
        }
    }
}
