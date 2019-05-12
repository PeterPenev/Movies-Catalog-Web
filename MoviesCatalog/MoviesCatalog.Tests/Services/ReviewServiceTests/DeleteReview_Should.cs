using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Services;
using MoviesCatalog.Services.Utils;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.ReviewServiceTests
{
    [TestClass]
    public class DeleteReview_Should
    {
        [TestMethod]
        public async Task ThrowExeption_WhenReviewNotFromUser()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExeption_WhenReviewNotFromUser));

            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Reviews.AddAsync(TestHelper.TestReview1());
                await arrangeContext.Users.AddAsync(TestHelper.TestUser2());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.DeleteReviewAsync(TestHelper.TestReview1().Id, TestHelper.TestUser2().Id));
                Assert.AreEqual(ex.Message, string.Format(ServicesConstants.ReviewNotFromUser, TestHelper.TestUser2().UserName));
            }
        }

        [TestMethod]
        public async Task Return_Succeed()
        {
            var options = TestUtils.GetOptions(nameof(Return_Succeed));

            using (var arrangeContext = new MoviesCatalogContext(options))
            {
               await arrangeContext.Reviews.AddAsync(TestHelper.TestReview1());
               await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var review = await sut.DeleteReviewAsync(TestHelper.TestReview1().Id, TestHelper.TestUser1().Id);

                Assert.IsTrue(review.IsDeleted);
            }
        }
    }
}
