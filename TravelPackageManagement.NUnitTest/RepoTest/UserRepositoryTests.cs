using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Implementations;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagement.NUnitTest.RepoTest
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private AppDbContext _context;
        private UserRepository _repo;

        [SetUp]
        public void Setup()
        {
            // Use a unique name for the In-Memory database for every test run
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repo = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Fix for NUnit1032: Explicitly dispose the context
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }

            // Fix for NUnit1032: Nullify the repository reference
            // This ensures no stale references are held between tests
            _repo = null;
        }

        [Test]
        public async Task AddAndGetUser_Works()
        {
            // ARRANGE
            var user = new User
            {
                Username = "test",
                Password = "pass",
                Email = "test@email.com"
            };

            // ACT
            await _repo.AddUserAsync(user);
            var result = await _repo.GetUserByUsernameOrEmailAsync("test");

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Username, Is.EqualTo("test"));
        }
    }
}