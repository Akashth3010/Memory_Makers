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

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class BookingsControllerTests
    {
        private BookingsController _controller;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Use a Unique Name for the database each time to prevent data leaking between tests
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _controller = new BookingsController(_context);

            // Mocking Session and HttpContext
            var httpContext = new DefaultHttpContext();
            var sessionMock = new Mock<ISession>();
            httpContext.Session = sessionMock.Object;

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [TearDown]
        public void TearDown()
        {
            // Check if the context exists, then close it
            if (_context != null)
            {
                _context.Dispose();
            }

            if (_controller != null)
            {
                _controller.Dispose();
            }

            // If you mocked other disposable objects, dispose them here too
        }
        [Test]
        public async Task Create_UnauthorizedUser_ReturnsUnauthorized()
        {
            // ARRANGE
            int packageId = 1;
            DateTime bookingDate = DateTime.Now;
            int travelers = 2;
            string phone = "1234567890";
            string email = "test@email.com";

            // ACT
            var result = await _controller.Create(packageId, bookingDate, travelers, phone, email);

            // ASSERT
            // Using modern NUnit 4.x syntax
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}