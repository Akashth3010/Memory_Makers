using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Implementations;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagement.NUnitTest.RepoTest
{
    [TestFixture]
    public class PackageRepositoryTests
    {
        private AppDbContext _context;
        private PackageRepository _repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _repo = new PackageRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        [Test]
        public async Task AddAndGetPackage_Works()
        {
            var package = new TravelPackage { PackageName = "Test", Price = 100, Duration = "2 days" };
            await _repo.AddPackage(package);
            var result = await _repo.GetPackageById(package.PackageId);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PackageName, Is.EqualTo("Test"));
        }

        [Test]
        public async Task GetAllPackages_ReturnsList()
        {
            await _repo.AddPackage(new TravelPackage { PackageName = "Test", Price = 100, Duration = "2 days" });
            var all = await _repo.GetAllPackages();
            Assert.That(all.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task UpdatePackage_ChangesData()
        {
            var package = new TravelPackage { PackageName = "Test", Price = 100, Duration = "2 days" };
            await _repo.AddPackage(package);
            package.Price = 200;
            await _repo.UpdatePackage(package);
            var updated = await _repo.GetPackageById(package.PackageId);
            Assert.That(updated.Price, Is.EqualTo(200));
        }

        [Test]
        public async Task DeletePackage_RemovesData()
        {
            var package = new TravelPackage { PackageName = "Test", Price = 100, Duration = "2 days" };
            await _repo.AddPackage(package);
            await _repo.DeletePackage(package.PackageId);
            var deleted = await _repo.GetPackageById(package.PackageId);
            Assert.That(deleted, Is.Null);
        }
    }
}
