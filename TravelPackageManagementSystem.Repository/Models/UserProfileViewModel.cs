using System;
using System.Collections.Generic;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Initials { get; set; } // We will generate this from Username

        // Stats
        public int BookingsCount { get; set; }
        public int ReviewsCount { get; set; }
        public int WishlistCount { get; set; }

        public List<UserBookingDisplay> RecentBookings { get; set; } = new List<UserBookingDisplay>();
    }

    public class UserBookingDisplay
    {
        public int BookingId { get; set; }
        public string PackageName { get; set; }
        public DateTime TravelDate { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}