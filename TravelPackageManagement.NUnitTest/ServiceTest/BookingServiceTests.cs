using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagement.NUnitTest.Service
{
    [TestFixture]
    public class BookingServiceTests
    {
        private Mock<IBookingRepository> _bookingRepoMock;
        private BookingService _service;

        [SetUp]
        public void Setup()
        {
            _bookingRepoMock = new Mock<IBookingRepository>();
            _service = new BookingService(_bookingRepoMock.Object);
        }

        [Test]
        public void CreateBooking_SetsStatusAndCallsAdd()
        {
            var booking = new Booking();
            _service.CreateBooking(booking);
            Assert.That(booking.Status, Is.EqualTo(BookingStatus.CONFIRMED));
            _bookingRepoMock.Verify(r => r.AddBooking(booking), Times.Once);
        }

        [Test]
        public void GetBookings_ReturnsAll()
        {
            var bookings = new List<Booking> { new Booking() };
            _bookingRepoMock.Setup(r => r.GetAllBookings()).Returns(bookings);
            var result = _service.GetBookings();
            Assert.That(result, Is.EqualTo(bookings));
        }
    }
}
