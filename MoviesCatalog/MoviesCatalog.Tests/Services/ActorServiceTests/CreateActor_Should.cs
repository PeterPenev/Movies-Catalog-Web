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
    public class CreateActor_Should
    {
        [TestMethod]
        public async Task Succeed_ReturnActor()
        {
            var options = TestUtils.GetOptions(nameof(Succeed_ReturnActor));

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ActorService(assertContext);

                var actor = await sut.CreateActorAsync("Brad", "Pit", "Very good actor");

                Assert.AreEqual(actor.FirstName, "Brad");
                Assert.AreEqual(actor.LastName, "Pit");
                Assert.AreEqual(actor.Biography, "Very good actor");
            }
        }
    }
}
