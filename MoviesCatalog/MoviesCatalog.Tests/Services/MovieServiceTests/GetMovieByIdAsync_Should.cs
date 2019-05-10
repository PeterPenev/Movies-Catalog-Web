﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class GetMovieByIdAsync_Should
    {
        [TestMethod]
        public async Task Succeed_Return()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_Return));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Movies.AddAsync(TestHelper.TestMovie100());

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new MovieService(assertContext);

                var getMovieById = await sut.GetMovieByIdAsync(100);

                Assert.AreEqual(getMovieById.Title, "Movie 100");
            }
        }
    }
}
