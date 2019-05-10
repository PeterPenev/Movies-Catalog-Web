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
    public class AllActorsByMovieAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnCollection()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnCollection));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Movies.AddAsync(TestHelper.TestMovie100());
                await arrangeContext.Actors.AddAsync(TestHelper.TestActor100());
                await arrangeContext.Actors.AddAsync(TestHelper.TestActor200());
                await arrangeContext.MoviesActors.AddAsync(TestHelper.TestMoviesActors100());
                await arrangeContext.MoviesActors.AddAsync(TestHelper.TestMoviesActors200());

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new MovieService(assertContext);
                
                var allActorsByMovie = await sut.AllActorsByMovieAsync(100);

                Assert.AreEqual(allActorsByMovie.Count,2);
            }
        }
    }
}
