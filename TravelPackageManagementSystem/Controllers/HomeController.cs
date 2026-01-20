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
        public async Task<IActionResult> Index()
        {
            // Pull the 4 main state cards for the top section
            var destinations = await _context.Destinations.ToListAsync();

            // Pull exactly the 12 packages marked as 'IsTrending = true'
            ViewBag.TrendingPackages = await _context.TravelPackages
                .Where(p => p.IsTrending == true)
                .ToListAsync();

            return View(destinations);
        }
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // Update your search or filter logic in HomeController
        public async Task<IActionResult> Destination(string state)
        {
            // Fix: Only pull packages for this state that are NOT marked as trending
            var packages = await _context.TravelPackages
                .Include(p => p.ParentDestination)
                    .ThenInclude(d => d.GalleryImages)
                .Where(p => p.ParentDestination.StateName.ToLower() == state.ToLower()
                         && p.IsTrending == false) // This is the fix
                .ToListAsync();

            ViewBag.StateName = state;
            return View("TopDestination/DestinationTD", packages);
        }

        // --- DYNAMIC BACKEND FETCHING ---
        public async Task<IActionResult> MeghalayaTD(string searchTerm, decimal? maxPrice)
        {
            // Fix: Include ParentDestination to access StateName
            var query = _context.TravelPackages
                .Include(p => p.ParentDestination)
                .Where(p => p.ParentDestination != null && p.ParentDestination.StateName == "Meghalaya");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.PackageName.Contains(searchTerm));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Ensure the view path is correct for your project structure
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

        //public async Task<IActionResult> MyBookings()
        //{
        //    // This fetches the data and passes it to the View
        //    var bookings = await _context.Bookings
        //                                 .Include(b => b.TravelPackage)
        //                                 .ToListAsync();

        //    return View(bookings);
        //}

        public async Task<IActionResult> MyBookings()
        {
            var bookings = await _context.Bookings
                .AsNoTracking() // Improves performance for read-only views
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
        // Action 1: Returns JSON list for the autocomplete dropdown
        //[HttpGet]
        [HttpGet]
        public async Task<JsonResult> GetSuggestions(string term)
        {
            if (string.IsNullOrEmpty(term)) return Json(new List<string>());

            // Fetches State names directly from your database
            var suggestions = await _context.Destinations
                .Where(d => d.StateName.Contains(term))
                .Select(d => d.StateName)
                .Take(5) // Keep the list short for better UI
                .ToListAsync();

            return Json(suggestions);
        }

        // Action 2: Processes the search and redirects to the correct page
        public async Task<IActionResult> Search(string destination)
        {
            if (string.IsNullOrEmpty(destination)) return RedirectToAction("Index");

            // Check if the user searched for a State (e.g., "Meghalaya")
            var stateMatch = await _context.Destinations
                .FirstOrDefaultAsync(d => d.StateName.ToLower() == destination.ToLower());

            if (stateMatch != null)
            {
                // Redirect to your existing Destination action with the state parameter
                return RedirectToAction("Destination", new { state = stateMatch.StateName });
            }

            // If not a state, find specific packages
            var results = await _context.TravelPackages
                .Include(p => p.ParentDestination)
                .Where(p => p.PackageName.Contains(destination) || p.Location.Contains(destination))
                .Where(p => !p.IsTrending) // Ensure we show standard results
                .ToListAsync();

            ViewBag.SearchTerm = destination;
            return View("TopDestination/DestinationTD", results);
        }
    }
}