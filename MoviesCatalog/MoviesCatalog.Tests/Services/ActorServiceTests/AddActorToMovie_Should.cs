using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using MoviesCatalog.Services;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.ActorServiceTests
{
    [TestClass]
    public class AddActorToMovie_Should
    {
        [TestMethod]
        public async Task Succeed_AddActorWhenDoesNotExistInMovie()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_AddActorWhenDoesNotExistInMovie));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
               await arrangeContext.Actors.AddAsync(TestHelper.TestActor1());
               await arrangeContext.Movies.AddAsync(TestHelper.TestMovie1());
               await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ActorService(assertContext);

                var movie = await sut.AddActorToMovieAsync(TestHelper.TestMovie1().Id, TestHelper.TestActor1().Id);

                Assert.AreEqual(movie.MoviesActors.Count, 1);
            }
        }

        [TestMethod]
        public async Task ThrowsExeption_WhenActorExistInMovie()
        {
            var options = TestUtils.GetOptions(nameof(ThrowsExeption_WhenActorExistInMovie));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
               
                await arrangeContext.MoviesActors.AddAsync(TestHelper.MoviesActors1());
                await arrangeContext.SaveChangesAsync();
               
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ActorService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.AddActorToMovieAsync(TestHelper.TestMovie1().Id, TestHelper.TestActor1().Id));

                //Assert.AreEqual(ex.Message, string.Format(ServicesConstants.UserAlreadyVoted,
                //                                           TestHelper.TestUser1().UserName, TestHelper.TestMovie1().Title));
            }
        }
    }
}
