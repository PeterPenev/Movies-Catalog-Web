using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Migrations;
using MoviesCatalog.Data.Models;
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
    public class CreateMovieAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnMovie()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnMovie));

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new MovieService(assertContext);

                DateTime releaseDate = DateTime.Now;
                                
                var movie = await sut.CreateMovieAsync("Movie 01", "Trailer", "Poster", null, "Description", releaseDate, "3c57e188 - 409f - 447b - a55d - 37e450df359e");

                var user = new ApplicationUser() { UserName = "peter" };

                movie.User= user;
                
                Assert.AreEqual(movie.Title, "Movie 01");
                Assert.AreEqual(movie.Trailer, "Trailer");
                Assert.AreEqual(movie.Poster, "Poster");
                Assert.AreEqual(movie.Description, "Description");
                Assert.AreEqual(movie.User.UserName, "peter");
                Assert.AreEqual(movie.ReleaseDate, releaseDate);                                
            }
        }
    }
}
