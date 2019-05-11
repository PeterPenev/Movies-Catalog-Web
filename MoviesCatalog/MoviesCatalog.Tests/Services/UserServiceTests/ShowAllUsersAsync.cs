using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoviesCatalog.Data;
using MoviesCatalog.Services;
using MoviesCatalog.Tests.Services.HelpersMethods;
using MoviesCatalog.Tests.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCatalog.Tests.Services.UserServiceTests
{
    [TestClass]
    public class ShowAllUsersAsync
    {
        [TestMethod]
        public async Task Return_RightCollection()
        {
            var options = TestUtils.GetOptions(nameof(Return_RightCollection));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Users.AddAsync(TestHelper.TestUser1());
                await arrangeContext.Users.AddAsync(TestHelper.TestUser2());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var mockedService = new Mock<IServiceProvider>();
                var sut = new UserService(assertContext, mockedService. Object);

                var users = await sut.ShowAllUsersAsync();

                Assert.AreEqual(users.Count, 2);
            }
        }

    }
}
