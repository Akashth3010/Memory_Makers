using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Host() => View();

        // ---------------- HOST PACKAGE SUBMISSION (FIXED & MERGED) ----------------
        [HttpPost]
        public async Task<IActionResult> SubmitPackage([FromBody] HostSubmissionViewModel model)
        {
            try
            {
                if (model == null) return Json(new { success = false, message = "Invalid data" });

                // 1. Create Host Detail
                var host = new HostContactDetail
                {
                    HostAgencyName = model.HostAgencyName,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    CityCountry = model.CityCountry
                };

                _context.HostContactDetails.Add(host);
                await _context.SaveChangesAsync(); // Generates host.Id

                // 2. Create Package Detail linked to Host
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
                    HostId = host.Id,
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

        // ---------------- TEAMMATE'S INDEX & TRENDING ----------------
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
        //{
        //    var package = await _context.TravelPackages.FindAsync(PackageId);
        //    if (package == null) return NotFound();

        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Identity.Name) ?? await _context.Users.FirstOrDefaultAsync();
        //    if (user == null) return Unauthorized();

        //    var booking = new Booking
        //    {
        //        PackageId = PackageId,
        //        UserId = user.UserId,
        //        BookingDate = DateTime.Now,
        //        TravelDate = TravelDate,
        //        Guests = Guests,
        //        ContactPhone = ContactPhone,
        //        TotalAmount = package.Price * Guests,
        //        Status = BookingStatus.PENDING
        //    };

        //    _context.Bookings.Add(booking);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("PaymentPage", new { bookingId = booking.BookingId });
        //}

        //[HttpPost]
        //public async Task<IActionResult> ConfirmPayment(int bookingId)
        //{
        //    var booking = await _context.Bookings.FindAsync(bookingId);
        //    if (booking != null)
        //    {
        //        booking.Status = BookingStatus.CONFIRMED;
        //        await _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction("MyBookings");
        //}
        public async Task<IActionResult> MyBookings()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // CHANGE: "Auth" par jao, par saath mein batao ki wapas "MyBookings" aana hai
                return RedirectToAction("Auth", "Home", new { returnUrl = "/Home/MyBookings" });
            }

            var myBookings = await _context.Bookings
                .AsNoTracking()
                .Include(b => b.TravelPackage)
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
        public async Task<IActionResult> MeghPack3(int id)
        {
            var package = await _context.TravelPackages.Include(p => p.Itineraries).FirstOrDefaultAsync(p => p.PackageId == id);
            if (package == null) return NotFound();
            return View("Package/MeghPack3", package);
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
        //public IActionResult aboutus() => View();

        //[HttpGet]
        //public IActionResult Auth()
        //{
        //    return View();
        //}
    }
}