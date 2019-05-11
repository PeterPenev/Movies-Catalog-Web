using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Tests.Services.Utils;
using MoviesCatalog.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoviesCatalog.Tests.Services.HelpersMethods;

namespace MoviesCatalog.Tests.Services.ReviewServiceTests
{
    [TestClass]
    public class GetReviewByIdAsync_Should
    {
        [TestMethod]
        public async Task Return_RightReview()
        {
            var options = TestUtils.GetOptions(nameof(Return_RightReview));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Reviews.AddAsync(TestHelper.TestReview1());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var review = await sut.GetReviewByIdAsync(1);

                Assert.AreEqual(review.Id, 1);
            }
        }

        [TestMethod]
        public async Task Return_Null()
        {
            var options = TestUtils.GetOptions(nameof(Return_Null));
         
            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ReviewService(assertContext);

                var review = await sut.GetReviewByIdAsync(1);

                Assert.IsNull(review);
            }
        }
    }
}

