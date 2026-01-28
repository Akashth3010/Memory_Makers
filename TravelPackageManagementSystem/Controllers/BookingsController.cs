using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using Microsoft.AspNetCore.Http; // Required for Session

namespace TravelPackageManagementSystem.Controllers
{
    public class BookingsController : Controller
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int PackageId, DateTime TravelDate, int Guests, string ContactPhone, string ContactEmail)
        {
            var sessionUserName = HttpContext.Session.GetString("UserName");

            // IF SESSION IS EXPIRED, REDIRECT TO LOGIN INSTEAD OF JUST CRASHING
            if (string.IsNullOrEmpty(sessionUserName))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == sessionUserName);
            if (user == null) return BadRequest("User record not found.");

            var package = await _context.TravelPackages.FindAsync(PackageId);

            var booking = new Booking
            {
                PackageId = PackageId,
                TravelDate = TravelDate,
                Guests = Guests,
                ContactPhone = ContactPhone,
                ContactEmail = ContactEmail,
                TotalAmount = (package?.Price ?? 0) * Guests,
                Status = BookingStatus.PENDING,
                UserId = user.UserId
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // This will now send the generated ID to Home/PaymentPage
            return RedirectToAction("PaymentPage", "Home", new { bookingId = booking.BookingId });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                booking.Status = BookingStatus.CONFIRMED;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("MyBookings");
        }
    }
}