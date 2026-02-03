using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Application.Controllers;
using TravelPackageManagementSystem.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class AdminControllerTests
    {
        private AdminController _controller;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            // 1. Use a unique name for each test database
            // This prevents data from one test interfering with another
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _controller = new AdminController(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            // 2. Properly clear and close the database context
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _controller.Dispose();
        }

        [Test]
        public void Dashboard_ReturnsView()
        {
            // ACT
            var result = _controller.Dashboard();

            // ASSERT
            // 3. FIX: Modern NUnit 4.x syntax for type checking
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }
    }
}