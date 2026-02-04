using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Repository.Interface;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Implementations;

namespace TravelPackageManagement.NUnitTest.Service
{
    [TestFixture]
    public class AuthModelServiceTests
    {
        private Mock<IUserRepository> _userRepoMock;
        private AuthModelService _service;

        [SetUp]
        public void Setup()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _service = new AuthModelService(_userRepoMock.Object);
        }

        [Test]
        public async Task RegisterUserAsync_ExistingUser_ReturnsFalse()
        {
            var model = new RegisterViewModel { Username = "test", Email = "test@test.com", Password = "pass" };
            _userRepoMock.Setup(r => r.GetUserByUsernameOrEmailAsync(model.Username)).ReturnsAsync(new User());
            var result = await _service.RegisterUserAsync(model);
            Assert.That(result.Success, Is.False);
        }
    }
}
