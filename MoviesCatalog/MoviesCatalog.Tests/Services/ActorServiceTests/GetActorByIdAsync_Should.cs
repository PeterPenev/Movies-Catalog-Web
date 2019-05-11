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
    public class GetActorByIdAsync_Should
    {
        [TestMethod]
        public async Task Return_RightActor()
        {
            var options = TestUtils.GetOptions(nameof(Return_RightActor));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Actors.AddAsync(TestHelper.TestActor1());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ActorService(assertContext);

                var actor = await sut.GetActorByIdAsync(TestHelper.TestActor1().Id);

                Assert.AreEqual(actor.Id, TestHelper.TestActor1().Id);
            }
        }

        [TestMethod]
        public async Task Return_Null()
        {
            var options = TestUtils.GetOptions(nameof(Return_RightActor));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                arrangeContext.Actors.Add(TestHelper.TestActor1());
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ActorService(assertContext);

                var actor = await sut.GetActorByIdAsync(TestHelper.TestActor1().Id);

                Assert.AreEqual(actor.Id, TestHelper.TestActor1().Id);
            }
        }
    }
}
