using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Interfaces;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Implementations;

namespace TravelPackageManagementSystem.Services.Interfaces
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public void CreateBooking(Booking booking)
        {
            booking.Status = BookingStatus.CONFIRMED;
            _bookingRepository.AddBooking(booking);
        }
        public List<Booking> GetBookings()
        {
            return _bookingRepository.GetAllBookings();
        }
        public Booking GetBooking(int id)
        {
            return _bookingRepository.GetBookingById(id);
        }
        public void UpdateBooking(Booking booking)
        {
            _bookingRepository.UpdateBooking(booking);
        }
        public void CancelBooking(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking != null)
            {
                booking.Status = BookingStatus.CANCELLED;
                _bookingRepository.UpdateBooking(booking);
            }
        }
        public void CancelBooking(Booking booking)
        {
            booking.Status = BookingStatus.CANCELLED;
            _bookingRepository.UpdateBooking(booking);
        }

        public void DeleteBooking(int id)
        {
            throw new NotImplementedException();
        }
    }
}
