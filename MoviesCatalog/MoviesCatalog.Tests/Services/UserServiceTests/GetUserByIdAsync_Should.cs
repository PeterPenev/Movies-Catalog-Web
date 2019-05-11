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
    public class GetUserByIdAsync_Should
    {
        [TestMethod]
        public async Task Return_RightUser()
        {
            var options = TestUtils.GetOptions(nameof(Return_RightUser));
            using (var arrangeContext = new MoviesCatalogContext(options))
            {
                await arrangeContext.Users.AddAsync(TestHelper.TestUser1());
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var mockedService = new Mock<IServiceProvider>();
                var sut = new UserService(assertContext, mockedService.Object);

                var user = await sut.GetUserByIdAsync(TestHelper.TestUser1().Id);

                Assert.AreEqual(user.Id, TestHelper.TestUser1().Id);
            }
        }

        [TestMethod]
        public async Task Return_Null()
        {
            var options = TestUtils.GetOptions(nameof(Return_Null));

            using (var assertContext = new MoviesCatalogContext(options))
            {
                var mockedService = new Mock<IServiceProvider>();
                var sut = new UserService(assertContext, mockedService.Object);

                var user = await sut.GetUserByIdAsync(TestHelper.TestUser1().Id);

                Assert.IsNull(user);
            }
        }
    }
}
