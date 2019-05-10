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
    public class IsGenreExistAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnCheck()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnCheck));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Genres.AddAsync(TestHelper.TestGenre1());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new GenreService(assertContext);

                var isGenreExists = await sut.IsGenreExistAsync("Drama");

                Assert.IsTrue(isGenreExists);
            }
        }
    }
}
