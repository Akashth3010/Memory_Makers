using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
//using TravelPackageManagementSystem.Application.Data;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Application.Models;

namespace TravelPackageManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Host() => View();

        [HttpPost]
        public async Task<IActionResult> SubmitPackage(HostSubmissionViewModel model)
        {
            try
            {
                // 1. Check if the Destination exists
                var destinationRecord = await _context.Destinations
                    .FirstOrDefaultAsync(d => d.StateName.ToLower() == model.Destination.Trim().ToLower());

                // 2. If it doesn't exist, create it using your model's exact properties
                if (destinationRecord == null)
                {
                    destinationRecord = new Destination
                    {
                        StateName = model.Destination.Trim(),
                        ImageUrl = "/lib/Image/default-destination.jpg", // Default image
                        HotelCount = 0,   // Initial value
                        HolidayCount = 1  // This is the first package for this destination
                    };
                    _context.Destinations.Add(destinationRecord);
                    await _context.SaveChangesAsync();
                }

                // 3. Create and Save Host Detail
                var host = new HostContactDetail
                {
                    HostAgencyName = model.HostAgencyName,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    CityCountry = model.CityCountry,
                };
                _context.HostContactDetails.Add(host);
                await _context.SaveChangesAsync();

                // 4. Create Travel Package linked to the new/existing Destination
                var package = new TravelPackage
                {
                    PackageName = model.PackageName,
                    Destination = model.Destination,
                    DestinationId = destinationRecord.DestinationId,
                    Location = model.Location ?? model.Destination,
                    PackageType = model.PackageType,
                    Duration = model.Duration,
                    Price = model.Price,
                    Description = model.Description,
                    HostId = host.Id,
                    IsTrending = false,
                    AvailabilityStatus = PackageStatus.AVAILABLE,
                    ApprovalStatus = ApprovalStatus.Pending // Hidden from public until Admin approves
                };

                _context.TravelPackages.Add(package);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Helpful for debugging in the browser console
                return Json(new { success = false, message = "Database Error: " + ex.InnerException?.Message ?? ex.Message });
            }
        }

        public IActionResult SubmissionSuccess()
        {
            return View(); // Create a simple view saying "Your package is under review"
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public HomeController(AppDbContext context)
        //{
        //    _context = context;
        //}
        public async Task<IActionResult> Index()
        {
            // Pull the 4 main state cards for the top section
            var destinations = await _context.Destinations.ToListAsync();

            // Pull exactly the 12 packages marked as 'IsTrending = true'
            ViewBag.TrendingPackages = await _context.TravelPackages
    .Where(p => p.IsTrending == true && p.ApprovalStatus == ApprovalStatus.Approved) // Add status check
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
    .Where(p => p.ParentDestination.StateName.ToLower() == state.ToLower()
             && p.IsTrending == false
             && p.ApprovalStatus == ApprovalStatus.Approved) // Add status check
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
        public async Task<IActionResult> CreateBooking(int PackageId, DateTime TravelDate, int Guests, string ContactPhone)
        {
            // 1. Fetch the actual package from DB to get the reliable Price
            var package = await _context.TravelPackages.FindAsync(PackageId);
            if (package == null) return NotFound();

            // 2. GET ACTUAL USER ID: Assuming the user is authenticated, 
            // we find them in the database by their Email/Username.
            // For now, let's fetch the first user or the one matching the session.
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Identity.Name)
                       ?? await _context.Users.FirstOrDefaultAsync(); // Fallback for testing

            // 3. SERVER-SIDE CALCULATION: Prevents the "0" amount error
            decimal finalTotal = package.Price * Guests;

            var booking = new Booking
            {
                PackageId = PackageId,
                UserId = user.UserId, // USE DYNAMIC ID
                BookingDate = DateTime.Now,
                TravelDate = TravelDate,
                Guests = Guests,
                ContactPhone = ContactPhone,
                TotalAmount = finalTotal, // Use the calculated variable
                Status = BookingStatus.PENDING
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

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
        //public IActionResult CustomerSupport()
        //{
        //    return View();
        //}

        //public IActionResult TravelGuide()
        //{
        //    return View();
        //}

        public IActionResult aboutus()
        {
            return View();
        }
    }
}