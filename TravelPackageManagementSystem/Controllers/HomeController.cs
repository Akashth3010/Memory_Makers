using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using System.Security.Cryptography; // Added for Hashing
using System.Text; // Added for Hashing

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

        // ---------------- HOST PACKAGE SUBMISSION (FIXED WITH SMART CHECK) ----------------
        [HttpPost]
        public async Task<IActionResult> SubmitPackage([FromBody] HostSubmissionViewModel model)
        {
            try
            {
                if (model == null) return Json(new { success = false, message = "Invalid data" });

                int hostIdForPackage;

                // 1. CHECK IF HOST IS ALREADY LOGGED IN
                int? loggedInHostId = HttpContext.Session.GetInt32("HostId");

                if (loggedInHostId.HasValue)
                {
                    // CASE A: Host is logged in -> Use their existing ID
                    hostIdForPackage = loggedInHostId.Value;
                }
                else
                {
                    // CASE B: Guest User -> Create new Host Contact Detail
                    var host = new HostContactDetail
                    {
                        HostAgencyName = model.HostAgencyName,
                        EmailAddress = model.EmailAddress,
                        PhoneNumber = model.PhoneNumber,
                        CityCountry = model.CityCountry,
                        // Hash the default password so DB doesn't reject it
                        Password = HashPassword("DefaultPassword123")
                    };

                    _context.HostContactDetails.Add(host);
                    await _context.SaveChangesAsync(); // Generates host.Id

                    hostIdForPackage = host.Id; // Use this new ID
                }

                // 2. Create Package Detail linked to the determined HostId
                var package = new TravelPackage
                {
                    PackageName = model.PackageName,
                    Destination = model.Destination,
                    DestinationId = model.DestinationId, // Handles int? properly
                    Location = model.Location ?? "Not Specified",
                    PackageType = model.PackageType,
                    Duration = model.Duration,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl ?? "",
                    ThumbnailUrl1 = model.ThumbnailUrl1 ?? "",
                    ThumbnailUrl2 = model.ThumbnailUrl2 ?? "",
                    ThumbnailUrl3 = model.ThumbnailUrl3 ?? "",
                    HostId = hostIdForPackage, // ✅ Links to either existing or new Host
                    AvailabilityStatus = PackageStatus.AVAILABLE,
                    ApprovalStatus = ApprovalStatus.Pending // ✅ Set to Pending (0)
                };

                _context.TravelPackages.Add(package);
                await _context.SaveChangesAsync(); // Generates package.PackageId

                // 3. Save Itineraries submitted by Host
                if (model.Itineraries != null && model.Itineraries.Count > 0)
                {
                    foreach (var item in model.Itineraries)
                    {
                        _context.Itineraries.Add(new Itinerary
                        {
                            PackageId = package.PackageId, // Link to the saved package
                            DayNumber = item.dayNumber,
                            ActivityTitle = item.title,
                            ActivityDescription = item.desc,
                            Inclusions = item.inclusions,
                            Exclusions = item.exclusions
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var package = await _context.TravelPackages
                .Include(p => p.Itineraries)
                .Include(p => p.Host) // CRITICAL: This pulls data from HostContactDetails
                .FirstOrDefaultAsync(m => m.PackageId == id);

            if (package == null) return NotFound();

            return View(package);
        }

        // ---------------- INDEX & TRENDING ----------------
        public async Task<IActionResult> Index()
        {
            var destinations = await _context.Destinations
                .AsNoTracking()
                .ToListAsync();

            var trendingPackages = await _context.TravelPackages
                .Where(p => p.ApprovalStatus == ApprovalStatus.Approved &&
                            p.AvailabilityStatus == PackageStatus.AVAILABLE &&
                            p.IsTrending == true)
                .OrderBy(p => p.PackageId)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.TrendingPackages = trendingPackages;

            return View(destinations);
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                ViewBag.UserWishlistIds = _context.Wishlists
                    .Where(w => w.UserId == userId)
                    .Select(w => w.PackageId)
                    .ToList();
            }
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // ---------------- TEAMMATE'S DESTINATION GALLERY ----------------
        public async Task<IActionResult> Destination(string state)
        {
            var packages = await _context.TravelPackages
                .Include(p => p.ParentDestination)
                    .ThenInclude(d => d.GalleryImages)
                .Where(p => p.ParentDestination.StateName.ToLower() == state.ToLower()
                         && p.IsTrending == false
                         && p.ApprovalStatus == ApprovalStatus.Approved)
                .ToListAsync();

            ViewBag.StateName = state;

            if (packages == null || !packages.Any())
            {
                return View("TopDestination/DestinationTD", new List<TravelPackage>());
            }

            return View("TopDestination/DestinationTD", packages);
        }

        public async Task<IActionResult> MeghalayaTD(string searchTerm, decimal? maxPrice)
        {
            var query = _context.TravelPackages.Where(p => p.Destination == "Meghalaya");
            if (!string.IsNullOrEmpty(searchTerm)) query = query.Where(p => p.PackageName.Contains(searchTerm));
            if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice.Value);
            return View("TopDestination/MeghalayaTD", await query.ToListAsync());
        }

        // ---------------- BOOKING & PAYMENT ----------------
        //[HttpPost]
        //public async Task<IActionResult> CreateBooking(int PackageId, DateTime TravelDate, int Guests, string ContactPhone)
        //{ ... }

        //    return View(myBookings);
        //}
        public async Task<IActionResult> MyBookings()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Auth", "Home", new { returnUrl = "/Home/MyBookings" });
            }

            var myBookings = await _context.Bookings
                .AsNoTracking()
                .Include(b => b.TravelPackage) // Fetches Package Name
                .Include(b => b.User)          // <--- THIS LINE IS MISSING! (Fetches Username)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();

            return View(myBookings);
        }

        // Auth Action ko update karein taaki wo URL accept kar sake
        public IActionResult Auth(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl; // URL ko View mein bhejna zaroori hai
            return View();
        }


        // ---------------- PACKAGE DETAILS & ITINERARY ----------------
        public async Task<IActionResult> PackageDetails(int id)
        {
            var package = await _context.TravelPackages.Include(p => p.Itineraries).FirstOrDefaultAsync(p => p.PackageId == id);
            if (package == null) return NotFound();
            return View("Package/PackageDetails", package);
        }

        [HttpGet]
        public IActionResult PaymentPage(int? bookingId)
        {
            if (bookingId == null) return RedirectToAction("Index");
            ViewBag.BookingId = bookingId;
            return View();
        }

        public IActionResult TravelGuide() => View();
        public IActionResult CustomerSupport() => View();

        // ---------------- TEAMMATE'S SEARCH SUGGESTIONS ----------------
        [HttpGet]
        public async Task<JsonResult> GetSuggestions(string term)
        {
            if (string.IsNullOrEmpty(term)) return Json(new List<string>());

            var suggestions = await _context.Destinations
                .Where(d => d.StateName.Contains(term))
                .Select(d => d.StateName)
                .Take(5)
                .ToListAsync();

            return Json(suggestions);
        }

        public async Task<IActionResult> Search(string destination)
        {
            if (string.IsNullOrEmpty(destination)) return RedirectToAction("Index");
            var stateMatch = await _context.Destinations
                .FirstOrDefaultAsync(d => d.StateName.ToLower() == destination.ToLower());
            if (stateMatch != null)
            {
                return RedirectToAction("Destination", new { state = stateMatch.StateName });
            }
            var results = await _context.TravelPackages
                .Include(p => p.ParentDestination)
                .Where(p => p.PackageName.Contains(destination) || p.Location.Contains(destination))
                .Where(p => !p.IsTrending)
                .ToListAsync();
            ViewBag.SearchTerm = destination;
            return View("TopDestination/DestinationTD", results);
        }

        public IActionResult Failure() => View("Trending/Failure");
        // Inside HomeController.cs
        public IActionResult Success(string txn, decimal amt, string pkg)
        {
            ViewBag.TransactionId = txn;
            ViewBag.Amount = amt;
            ViewBag.PackageName = pkg; // Ensure this is assigned!
            return View("~/Views/Home/Trending/Success.cshtml");
        }

        // ===========================================================
        // NEW: HOST ACCOUNT & PROFILE LOGIC (SECURED)
        // ===========================================================

        [HttpPost]
        public async Task<IActionResult> RegisterHost([FromBody] HostContactDetail model)
        {
            try
            {
                if (_context.HostContactDetails.Any(h => h.EmailAddress == model.EmailAddress))
                    return Json(new { success = false, message = "This email is already registered." });

                // SECURE: Hash the password before saving
                model.Password = HashPassword(model.Password);

                _context.HostContactDetails.Add(model);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Account created successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginHost([FromBody] LoginModel model)
        {
            // SECURE: Hash the input password to compare with database
            var hashedPassword = HashPassword(model.Password);

            var host = await _context.HostContactDetails
                .FirstOrDefaultAsync(h => h.EmailAddress == model.Email && h.Password == hashedPassword);

            if (host != null)
            {
                HttpContext.Session.SetInt32("HostId", host.Id);
                HttpContext.Session.SetString("HostName", host.HostAgencyName);
                return Json(new { success = true, message = "Welcome back, " + host.HostAgencyName });
            }
            return Json(new { success = false, message = "Invalid email or password." });
        }

        public async Task<IActionResult> HostProfile()
        {
            int? hostId = HttpContext.Session.GetInt32("HostId");
            if (hostId == null) return RedirectToAction("Host");

            var host = await _context.HostContactDetails
                .Include(h => h.TravelPackages)
                .FirstOrDefaultAsync(h => h.Id == hostId);

            return View(host);
        }

        public IActionResult LogoutHost()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Host");
        }

        // --- SECURITY HELPER ---
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}