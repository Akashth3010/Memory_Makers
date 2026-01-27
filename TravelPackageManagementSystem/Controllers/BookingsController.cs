using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data; // Ensure this is your correct Data namespace
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Controllers
{
    public class BookingsController : Controller
    {
        // Fix: Declaring _context inside the class resolves "does not exist in current context"
        private readonly AppDbContext _context;

        // Fix: Constructor for Dependency Injection - required for _context to work
        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // Fix: Moved 'Create' out of any other method so it's a proper Action, not a local function
        [HttpPost]
        public async Task<IActionResult> Create(int PackageId, DateTime BookingDate)
        {
            var booking = new Booking
            {
                PackageId = PackageId,
                BookingDate = BookingDate,
                // Fix: Uses the Enum 'BookingStatus.PENDING' instead of a string "PENDING"
                Status = BookingStatus.PENDING,
                UserId = 1 // Hardcoded for now
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Fix: RedirectToAction now works because it is inside the Controller class
            return RedirectToAction("PaymentPage", "Home");
        }

        public async Task<IActionResult> MyBookings()
        {
            // Fix: Added .Include to handle the non-nullable TravelPackage property warning
            var bookings = await _context.Bookings
                .Include(b => b.TravelPackage)
                .Where(b => b.UserId == 1)
                .ToListAsync();

            return View(bookings);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetPackagePrice(int bookingId)
        //{
        //    // Find the booking and its associated package
        //    var booking = await _context.Bookings
        //        .Include(b => b.Package)
        //        .FirstOrDefaultAsync(b => b.BookingId == bookingId);

        //    if (booking == null) return NotFound();

        //    // Return the real price from the database
        //    return Ok(new { dbPrice = booking.Package.Price });
        //}
    }
}