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
    public class CreateGenreAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnGenre()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnGenre));

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new GenreService(assertContext);

                var genre = await sut.CreateGenreAsync("drama");

                Assert.AreEqual(genre.Name, "drama");
            }
        }
    }
}
