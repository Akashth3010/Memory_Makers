using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Implementations;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagement.NUnitTest.RepoTest
{
    [TestFixture]
    public class BookingRepositoryTests
    {
        private AppDbContext _context;
        private BookingRepository _repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _repo = new BookingRepository(_context);
        }

        // --- FIX FOR NUnit1032 STARTS HERE ---
        [TearDown]
        public void TearDown()
        {
            // Dispose the database context to release resources
            _context?.Dispose();

            // Nullify the repository reference to clear the memory path
            _repo = null;
        }
        // --- FIX FOR NUnit1032 ENDS HERE ---

        [Test]
        public void AddAndGetBooking_Works()
        {
            var booking = new Booking { PackageId = 1, TravelDate = DateTime.Now, Guests = 2, ContactPhone = "123", ContactEmail = "a@b.com", TotalAmount = 100, Status = BookingStatus.PENDING, UserId = 1 };
            _repo.AddBooking(booking);
            var result = _repo.GetBookingById(booking.BookingId);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PackageId, Is.EqualTo(1));
        }

        [Test]
        public void GetAllBookings_ReturnsList()
        {
            _repo.AddBooking(new Booking { PackageId = 1, TravelDate = DateTime.Now, Guests = 2, ContactPhone = "123", ContactEmail = "a@b.com", TotalAmount = 100, Status = BookingStatus.PENDING, UserId = 1 });
            var all = _repo.GetAllBookings();
            Assert.That(all.Count, Is.GreaterThan(0));
        }

        [Test]
        public void UpdateBooking_ChangesData()
        {
            var booking = new Booking { PackageId = 1, TravelDate = DateTime.Now, Guests = 2, ContactPhone = "123", ContactEmail = "a@b.com", TotalAmount = 100, Status = BookingStatus.PENDING, UserId = 1 };
            _repo.AddBooking(booking);
            booking.Guests = 5;
            _repo.UpdateBooking(booking);
            var updated = _repo.GetBookingById(booking.BookingId);
            Assert.That(updated.Guests, Is.EqualTo(5));
        }

        [Test]
        public void DeleteBooking_RemovesData()
        {
            var booking = new Booking { PackageId = 1, TravelDate = DateTime.Now, Guests = 2, ContactPhone = "123", ContactEmail = "a@b.com", TotalAmount = 100, Status = BookingStatus.PENDING, UserId = 1 };
            _repo.AddBooking(booking);
            _repo.DeleteBooking(booking.BookingId);
            var deleted = _repo.GetBookingById(booking.BookingId);
            Assert.That(deleted, Is.Null);
        }
    }
}