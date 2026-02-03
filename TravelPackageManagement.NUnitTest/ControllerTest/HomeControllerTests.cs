using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TravelPackageManagementSystem.Controllers;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Use Guid.NewGuid() to ensure a completely fresh database for every test
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _controller = new HomeController(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            // Wipe the in-memory data and release resources
            _context.Database.EnsureDeleted();
            _context.Dispose();

            if (_controller != null)
            {
                _controller.Dispose();
                _controller = null;
            }

            // Note: Controllers are usually disposed by the framework, 
            // but disposing here is fine if you aren't using a mock factory.
        }

        [Test]
        public async Task SubmitPackage_NullModel_ReturnsJsonError()
        {
            // ACT
            var result = await _controller.SubmitPackage(null);

            // ASSERT
            // Verifies the result is specifically a JsonResult
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }
    }
}