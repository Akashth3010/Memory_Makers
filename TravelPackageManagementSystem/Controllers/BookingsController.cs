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
            if (string.IsNullOrEmpty(sessionUserName)) return Unauthorized();

            // Find the real User record
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == sessionUserName);
            if (user == null) return BadRequest();

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
                UserId = user.UserId // Save the REAL ID here
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("PaymentPage", "Home", new { bookingId = booking.BookingId });
        }

        public async Task<IActionResult> MyBookings()
        {
            // 1. Get the username from the Session (set during login)
            var sessionUserName = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(sessionUserName))
            {
                return RedirectToAction("Index", "Home"); // Redirect if not logged in
            }

            // 2. Fetch bookings where the linked User's Username matches the Session
            var bookings = await _context.Bookings
                .Include(b => b.TravelPackage)
                .Include(b => b.User)
                .Where(b => b.User.Username == sessionUserName) // Filter dynamically!
                .ToListAsync();

            return View(bookings);
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