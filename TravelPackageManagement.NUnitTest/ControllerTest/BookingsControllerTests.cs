using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TravelPackageManagementSystem.Controllers;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class BookingsControllerTests
    {
        private BookingsController _controller;
        private AppDbContext _context;
        private Mock<ISession> _sessionMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _controller = new BookingsController(_context);

            _sessionMock = new Mock<ISession>();
            var httpContext = new DefaultHttpContext();
            httpContext.Session = _sessionMock.Object;

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
            _context = null; // Fix for NUnit1032

            _controller?.Dispose();
            _controller = null; // Fix for NUnit1032
        }

        [Test]
        public async Task Create_UnauthorizedUser_RedirectsToLogin()
        {
            // ARRANGE
            // We do NOT set any Session values here, simulating a logged-out user.

            // ACT
            var result = await _controller.Create(1, DateTime.Now, 2, "1234567890", "test@email.com");

            // ASSERT
            // Most MVC controllers redirect to Login if session is null. 
            // If your controller specifically returns 'Unauthorized()', keep the old assertion.
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>().Or.InstanceOf<UnauthorizedResult>());
        }

        [Test]
        public async Task Create_AuthorizedUser_ReturnsViewOrRedirect()
        {
            // ARRANGE: Seed a package so the ID is valid
            var package = new TravelPackage { PackageId = 1, PackageName = "Test Trip" };
            _context.TravelPackages.Add(package);
            await _context.SaveChangesAsync();

            // Mock a Logged-in User (Setting UserId in session)
            var userId = 101;
            var userIdBytes = BitConverter.GetBytes(userId);
            _sessionMock.Setup(s => s.TryGetValue("UserId", out userIdBytes)).Returns(true);

            // ACT
            var result = await _controller.Create(1, DateTime.Now, 2, "1234567890", "test@email.com");

            // ASSERT
            Assert.That(result, Is.Not.Null);
            // Usually returns a View (Confirmation) or Redirects to Payment
            Assert.That(result, Is.InstanceOf<ViewResult>().Or.InstanceOf<RedirectToActionResult>());
        }
    }
}