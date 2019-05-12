using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesCatalog.Data;
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
    public class UpdateActorAsync_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnActor()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnActor));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Actors.AddAsync(TestHelper.TestActor1());
                await arrangeContext.SaveChangesAsync();
            }
            
            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ActorService(assertContext);

                var actor = await sut.UpdateActorAsync(TestHelper.TestActor1(), "image1.jpg", "Very nice one");

                Assert.AreEqual(actor.Biography, "Very nice one");
                Assert.AreEqual(actor.Picture, "image1.jpg");
            }
        }
    }
}
