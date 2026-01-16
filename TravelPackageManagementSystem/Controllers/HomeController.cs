using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // --- DYNAMIC BACKEND FETCHING ---
        public async Task<IActionResult> MeghalayaTD(string searchTerm, decimal? maxPrice)
        {
            var query = _context.TravelPackages.Where(p => p.Destination == "Meghalaya");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.PackageName.Contains(searchTerm));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Fix: Add the subfolder path "TopDestination/" before the view name
            return View("TopDestination/MeghalayaTD", await query.ToListAsync());
        }
        // --- BOOKING LOGIC ---
        [HttpPost]
        public async Task<IActionResult> CreateBooking(int PackageId, DateTime BookingDate)
        {
            var booking = new Booking
            {
                PackageId = PackageId,
                BookingDate = BookingDate,
                // Booking starts as PENDING until payment is confirmed
                Status = BookingStatus.PENDING,
                UserId = 1
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Pass the new BookingId to the payment page via ViewBag
            ViewBag.BookingId = booking.BookingId;
            return RedirectToAction("PaymentPage", new { bookingId = booking.BookingId });
        }

        // --- PAYMENT CONFIRMATION LOGIC ---
        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(int bookingId)
        {
            // 1. Find the specific booking in the database
            var booking = await _context.Bookings.FindAsync(bookingId);

            if (booking != null)
            {
                // 2. Update the status from PENDING to CONFIRMED
                booking.Status = BookingStatus.CONFIRMED;

                // 3. Save changes to SQL Server
                await _context.SaveChangesAsync();
            }

            // 4. Redirect to the Action to ensure the model is loaded for the view
            return RedirectToAction("MyBookings");
        }

        // --- DYNAMIC BOOKINGS VIEW ---

        public async Task<IActionResult> MyBookings()
        {
            // This fetches the data and passes it to the View
            var bookings = await _context.Bookings
                                         .Include(b => b.TravelPackage)
                                         .ToListAsync();

            return View(bookings);
        }

        // --- EXISTING ROUTES PRESERVED ---
        public IActionResult Hero() => View();
        public IActionResult Vrindavan() => View("Trending/Vrindavan");
        public IActionResult Rameshwaram() => View("Trending/Rameshwaram");
        public IActionResult Darjiling() => View("Trending/Darjiling");
        public IActionResult Tamilnadu() => View();

        public IActionResult TamilnaduTD(string id)
        {
            ViewBag.PackageId = id;
            return View("TopDestination/TamilnaduTD");
        }

        public IActionResult KeralaTD(string id)
        {
            ViewBag.PackageId = id;
            return View("TopDestination/KeralaTD");
        }

        public IActionResult MizoramTD(string id)
        {
            ViewBag.PackageId = id;
            return View("TopDestination/MizoramTD");
        }

        public IActionResult GoaTD() => View("TopDestination/GoaTD");
        public IActionResult UttarakhandTD() => View("TopDestination/uttarakhandTD");

        public IActionResult MeghPack1() => View("Package/MeghPack1");
        public IActionResult MeghPack2() => View("Package/MeghPack2");
        public async Task<IActionResult> MeghPack3(int id)
        {
            // Fix: Explicitly include the Itineraries collection
            var package = await _context.TravelPackages
                                         .Include(p => p.Itineraries)
                                         .FirstOrDefaultAsync(p => p.PackageId == id);

            if (package == null)
            {
                return NotFound();
            }

            return View("Package/MeghPack3", package);
        }

        public IActionResult GoaPack1() => View("Package/GoaPack1");
        public IActionResult GoaPack2() => View("Package/GoaPack2");
        public IActionResult GoaPack3() => View("Package/GoaPack3");

        public IActionResult PaymentPage(int? bookingId)
        {
            ViewBag.BookingId = bookingId;
            return View();
        }

        public IActionResult TravelGuide() => View();
        public IActionResult CustomerSupport() => View();
    }
}