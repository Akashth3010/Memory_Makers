using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Services.Implementations
{
    public interface IBookingService
    {
        void CreateBooking(Booking booking);
        List<Booking> GetBookings();
        Booking GetBooking(int id);
        void UpdateBooking(Booking booking);
        void CancelBooking(int id);
        void DeleteBooking(int id);
    }
}
    