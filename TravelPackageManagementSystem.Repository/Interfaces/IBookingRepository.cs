using System;
using System.Collections.Generic;
using System.Text;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Interfaces
{
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        List<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int id);
    }
}
