
using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data; // Ensure this matches your project
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Services.Implementations;

namespace TravelPackageManagement.NUnitTest.Tests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private PaymentService _service;
        private Mock<IPaymentRepository> _mockRepo;

        // FIX: Declaring the variable here makes it exist for the whole class
        private AppDbContext _tempContext;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IPaymentRepository>();

            // Initialize the In-Memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _tempContext = new AppDbContext(options);

            // Pass the mock repo and the real (but in-memory) context to the service
            _service = new PaymentService(_mockRepo.Object, _tempContext);
        }

        [TearDown]
        public void Cleanup()
        {
            // Clean up the database after each test
            _tempContext.Dispose();
        }

        [Test]
        public async Task ProcessPaymentAsync_ShouldReturnTrue_WhenBookingExists()
        {
            // 1. ARRANGE
            var bookingId = 1;

            // Add a dummy booking so the Service's FindAsync() doesn't return null
            var existingBooking = new Booking { BookingId = bookingId, Status = BookingStatus.PENDING };
            _tempContext.Bookings.Add(existingBooking);
            await _tempContext.SaveChangesAsync();

            var payment = new Payment
            {
                Amount = 100,
                PaymentMethod = "Card",
                TransactionId = "T123",
                BookingId = bookingId,
                Status = "Pending"
            };

            _mockRepo.Setup(r => r.AddPaymentAsync(It.IsAny<Payment>()))
                     .Returns(Task.CompletedTask);

            // 2. ACT
            var result = await _service.ProcessPaymentAsync(payment);

            // 3. ASSERT
            Assert.That(result, Is.True);

            // Check if status changed in the DB
            var updatedBooking = await _tempContext.Bookings.FindAsync(bookingId);
            Assert.That(updatedBooking.Status, Is.EqualTo(BookingStatus.CONFIRMED));
        }
    }
}
