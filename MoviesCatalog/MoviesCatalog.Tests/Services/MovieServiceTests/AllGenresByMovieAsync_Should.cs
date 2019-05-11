using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Services;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.MovieServiceTests
{
    [TestClass]
    public class AllGenresByMovieAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnCollection()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnCollection));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Movies.AddAsync(TestHelper.TestMovie100());
                await arrangeContext.Genres.AddAsync(TestHelper.TestGenre1());
                await arrangeContext.Genres.AddAsync(TestHelper.TestGenre2());
                await arrangeContext.MoviesGenres.AddAsync(TestHelper.MoviesGenres1());
                await arrangeContext.MoviesGenres.AddAsync(TestHelper.MoviesGenres3());

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new MovieService(assertContext);
                
                var allGenresByMovie = await sut.AllGenresByMovieAsync(100);

                Assert.AreEqual(allGenresByMovie.Count,2);
            }
        }
    }
}
