using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Services;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.MovieServiceTests
{
    [TestClass]
    public class ShowAllMoviesOrderedDescByRatingAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnCollection()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnCollection));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Movies.AddAsync(TestHelper.TestMovie100());
                await arrangeContext.Movies.AddAsync(TestHelper.TestMovie200());

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new MovieService(assertContext);

                var showAllMoviesOrderedDescByRating = await sut.ShowAllMoviesOrderedDescByRatingAsync();

                Assert.AreEqual(showAllMoviesOrderedDescByRating.Count, 2);
            }
        }
    }
}
