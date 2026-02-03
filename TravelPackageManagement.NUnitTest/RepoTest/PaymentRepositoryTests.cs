using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Implementations;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class PaymentRepositoryTests
    {
        private PaymentRepository _repository;
        private AppDbContext _context;
        [TearDown] // This tells NUnit to run this after every test
        public void TearDown()
        {
            if (_context != null)
            {
                _context.Dispose(); // This fixes NUnit1032
            }
        }

        [SetUp]
        public void Setup()
        {
            // Create a fresh "In-Memory" database for this test
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new PaymentRepository(_context);
        }

        [Test]
        public async Task AddPaymentAsync_ShouldActuallySaveData()
        {
            // ARRANGE - Give ALL required information
            var payment = new Payment
            {
                TransactionId = "REAL_SAVE_TEST",
                Amount = 50,
                BookingId = 1,
                Status = "Completed",
                PaymentMethod = "UPI",
                PaymentDate = System.DateTime.Now
            };

            // ACT
            await _repository.AddPaymentAsync(payment);
            await _repository.SaveChangesAsync();

            // ASSERT
            var allPayments = await _repository.GetAllPaymentsAsync();
            Assert.That(allPayments.Any(p => p.TransactionId == "REAL_SAVE_TEST"), Is.True);
        }
    }
}