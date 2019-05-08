using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Services;
using MoviesCatalog.Services.Utils;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.ReviewServiceTests
{
    [TestClass]
    public class DeleteReview_Should
    {
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

                var result = await sut.DeleteReviewAsync(TestHelper.TestReview1().Id, TestHelper.TestUser2().Id);

               var ex = Assert.ThrowsException<ArgumentException>(() => result);
                Assert.AreEqual(ex.Message, string.Format(ServicesConstants.ReviewNotFromUser,
                                                          TestHelper.TestUser2().UserName));


            }
        }
    }
}
