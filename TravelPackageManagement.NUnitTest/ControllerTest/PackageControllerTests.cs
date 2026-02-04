using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Controllers; // Adjust based on your actual namespace
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class PackageControllerTests
    {
        private PackageController _controller;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Create a unique In-Memory Database for each test
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            // Seed Data: We need a Destination and a Package to test the LINQ Joins
            var destination = new Destination { DestinationId = 1, StateName = "Meghalaya" };
            var package = new TravelPackage
            {
                PackageId = 1,
                PackageName = "Shillong Special",
                DestinationId = 1,
                ParentDestination = destination
            };

            _context.Destinations.Add(destination);
            _context.TravelPackages.Add(package);
            _context.SaveChanges();

            _controller = new PackageController(_context);
        }

        [Test]
        public async Task Index_ValidState_ReturnsViewWithPackages()
        {
            // ACT
            var result = await _controller.Index("Meghalaya") as ViewResult;

            // ASSERT
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<TravelPackage>;
            Assert.That(model.Count, Is.EqualTo(1));
            Assert.That(model[0].PackageName, Is.EqualTo("Shillong Special"));
            Assert.That(result.ViewData["StateName"], Is.EqualTo("Meghalaya"));
        }

        [Test]
        public async Task Index_InvalidState_ReturnsEmptyList()
        {
            // ACT
            var result = await _controller.Index("Kerala") as ViewResult;

            // ASSERT
            var model = result.Model as List<TravelPackage>;
            Assert.That(model.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task Details_ExistingId_ReturnsViewWithPackage()
        {
            // ACT
            var result = await _controller.Details(1) as ViewResult;

            // ASSERT
            Assert.That(result, Is.Not.Null);
            var model = result.Model as TravelPackage;
            Assert.That(model.PackageId, Is.EqualTo(1));
            Assert.That(model.ParentDestination.StateName, Is.EqualTo("Meghalaya"));
        }

        [Test]
        public async Task Details_NonExistingId_ReturnsNotFound()
        {
            // ACT
            var result = await _controller.Details(999);

            // ASSERT
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [TearDown]
        public void Cleanup()
        {
            _controller.Dispose();
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}