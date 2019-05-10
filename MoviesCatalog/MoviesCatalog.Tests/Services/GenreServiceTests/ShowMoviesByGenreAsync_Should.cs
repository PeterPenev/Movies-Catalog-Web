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
    public class ShowMoviesByGenreAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnCollection()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnCollection));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Genres.AddAsync(TestHelper.TestGenre1());
                await arrangeContext.Movies.AddAsync(TestHelper.TestMovie100());
                await arrangeContext.Movies.AddAsync(TestHelper.TestMovie200());
                await arrangeContext.MoviesGenres.AddAsync(TestHelper.MoviesGenres1());
                await arrangeContext.MoviesGenres.AddAsync(TestHelper.MoviesGenres2());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new GenreService(assertContext);

                var moviesByGenre = await sut.ShowMoviesByGenreAsync("Drama");                

                Assert.AreEqual(moviesByGenre.Count,2);
            }
        }
    }
}
