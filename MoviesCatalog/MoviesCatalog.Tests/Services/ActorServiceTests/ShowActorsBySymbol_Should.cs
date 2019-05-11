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
    public class ShowActorsBySymbol_Should
    {
        [TestMethod]
        public async Task Return_RightCollection()
        {
            var options = TestUtils.GetOptions(nameof(Return_RightCollection));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Actors.AddAsync(TestHelper.TestActor1());
                await arrangeContext.Actors.AddAsync(TestHelper.TestActor3());
                await arrangeContext.Actors.AddAsync(TestHelper.TestActor4());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var sut = new ActorService(assertContext);
                char symbol = 'i';
                var actors = await sut.ShowActorsStartWithSymbolAsync(symbol);

                Assert.AreEqual(actors.Count, 2);
            }
        }
    }
}
