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
            // 1.Create Host Detail
            var host = new HostContactDetail
            {
                HostAgencyName = model.HostAgencyName,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                CityCountry = model.CityCountry,
            };
            _context.HostContactDetails.Add(host);
            await _context.SaveChangesAsync();

            //2.Create Package Detail linked to Host
            var package = new TravelPackage
            {
                PackageName = model.PackageName,
                Destination = model.Destination,
                Location = model.Location,
                PackageType = model.PackageType,
                Duration = model.Duration,
                Price = model.Price,
                Description = model.Description,
                HostId = host.Id,
                AvailabilityStatus = PackageStatus.AVAILABLE,
                ApprovalStatus = ApprovalStatus.Pending
            };

            _context.TravelPackages.Add(package);
            await _context.SaveChangesAsync();

            return Json(new { Success = true });
        }
        public IActionResult Index()
        {
            return View();
        }

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

        public IActionResult CustomerSupport()
        {
            return View();
        }

        public IActionResult TravelGuide()
        {
            return View();
        }

        public IActionResult aboutus()
        {
            return View();
        }
    }
}