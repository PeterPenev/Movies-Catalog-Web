using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Services;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.GenreServiceTests
{
    [TestClass]
    public class GetAllGenresAsync_Should
    {
        [TestMethod]
        public async Task Return_RightCollection()
        {
            var options = TestUtils.GetOptions(nameof(Return_RightCollection));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Genres.AddAsync(TestHelper.TestGenre1());
                await arrangeContext.Genres.AddAsync(TestHelper.TestGenre2());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new GenreService(assertContext);

                var genres = await sut.GetAllGenresAsync();

                Assert.AreEqual(genres.Count, 2);
            }
        }
    }
}
