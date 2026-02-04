using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TravelPackageManagementSystem.Application.Controllers;
using TravelPackageManagementSystem.Services.Interfaces;
using TravelPackageManagementSystem.Repository.Models;
using System.Threading.Tasks;

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<IAuthModelService> _mockAuthService;
        private Mock<ISession> _mockSession;

        [SetUp]
        public void Setup()
        {
            _mockAuthService = new Mock<IAuthModelService>();
            _mockSession = new Mock<ISession>();
            _controller = new AccountController(_mockAuthService.Object);

            var httpContext = new DefaultHttpContext();
            httpContext.Session = _mockSession.Object;
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsOk()
        {
            var loginModel = new LoginViewModel { Username = "test", Password = "password" };

            // FIX: Changed UserRole.Customer to a more common name. 
            // CHANGE THIS to match your actual Enum (e.g., UserRole.USER)
            var fakeUser = new User { UserId = 1, Username = "test", Role = UserRole.CUSTOMER };

            _mockAuthService.Setup(s => s.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(fakeUser);

            var result = await _controller.Login(loginModel);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Register_Success_ReturnsOk()
        {
            var regModel = new RegisterViewModel { Username = "new" };
            var registrationResult = (Success: true, Message: null as string, User: new User { Username = "new", Role = UserRole.CUSTOMER });

            _mockAuthService.Setup(s => s.RegisterUserAsync(regModel))
                            .ReturnsAsync(registrationResult);

            var result = await _controller.Register(regModel);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [TearDown]
        public void Cleanup() => _controller?.Dispose();
    }

    // This makes 'RegistrationResult' visible to the test
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }
}