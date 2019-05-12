using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Services;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.ReviewServiceTests
{
    
    //[TestClass]
    //public class DidUserAlreadyVotedAsync_Should
    //{
    //    [TestMethod]
    //    public async Task Return_True()
    //    {
    //        var options = TestUtils.GetOptions(nameof(Return_True));
    //        using (var arrangeContext = new MoviesCatalogContext(options))
    //        {
    //            arrangeContext.Reviews.Add(TestHelper.TestReview1());
    //            arrangeContext.Movies.Add(TestHelper.TestMovie1());
    //            arrangeContext.Users.Add(TestHelper.TestUser1());
    //            arrangeContext.SaveChanges();
    //        }

    //        using (var assertContext = new MoviesCatalogContext(options))
    //        {
    //            var sut = new ReviewService(assertContext);
    //            Assert.IsTrue(await sut.DidUserAlreadyVoteForMovieAsync
    //                                   (1, TestHelper.TestUser1().Id));
    //        }
    //    }
    //}
}

