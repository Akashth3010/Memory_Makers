using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelPackageManagementSystem.Application.Models;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using Microsoft.EntityFrameworkCore;

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
                var host = new HostContactDetail
                {
                    HostAgencyName = model.HostAgencyName,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    CityCountry = model.CityCountry
                };

                _context.HostContactDetails.Add(host);
                await _context.SaveChangesAsync();

                var package = new TravelPackage
                {
                    PackageName = model.PackageName,
                    Destination = model.Destination,
                    DestinationId = model.DestinationId,
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
                    ApprovalStatus = ApprovalStatus.Pending
                };

                _context.TravelPackages.Add(package);
                await _context.SaveChangesAsync();

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
            catch (Exception)
            {
                return Json(new { success = false, message = "Internal error during submission." });
            }
        }

        public IActionResult Index() => View();
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> MeghalayaTD(string searchTerm, decimal? maxPrice)
        {
            var query = _context.TravelPackages.Where(p => p.Destination == "Meghalaya");
            if (!string.IsNullOrEmpty(searchTerm)) query = query.Where(p => p.PackageName.Contains(searchTerm));
            if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice.Value);
            return View("TopDestination/MeghalayaTD", await query.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(int PackageId, DateTime TravelDate, int Guests, string ContactPhone)
        {
            var package = await _context.TravelPackages.FindAsync(PackageId);
            if (package == null) return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Identity.Name) ?? await _context.Users.FirstOrDefaultAsync();
            if (user == null) return Unauthorized();

            var booking = new Booking
            {
                PackageId = PackageId,
                UserId = user.UserId,
                BookingDate = DateTime.Now,
                TravelDate = TravelDate,
                Guests = Guests,
                ContactPhone = ContactPhone,
                TotalAmount = package.Price * Guests,
                Status = BookingStatus.PENDING
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("PaymentPage", new { bookingId = booking.BookingId });
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

        public async Task<IActionResult> MyBookings()
        {
            var bookings = await _context.Bookings.Include(b => b.TravelPackage).ToListAsync();
            return View(bookings);
        }

        public IActionResult Hero() => View();
        public IActionResult Vrindavan() => View("Trending/Vrindavan");
        public IActionResult Rameshwaram() => View("Trending/Rameshwaram");
        public IActionResult Darjiling() => View("Trending/Darjiling");
        public IActionResult Tamilnadu() => View();
        public IActionResult TamilnaduTD(string id) { ViewBag.PackageId = id; return View("TopDestination/TamilnaduTD"); }
        public IActionResult KeralaTD(string id) { ViewBag.PackageId = id; return View("TopDestination/KeralaTD"); }
        public IActionResult MizoramTD(string id) { ViewBag.PackageId = id; return View("TopDestination/MizoramTD"); }
        public IActionResult GoaTD() => View("TopDestination/GoaTD");
        public IActionResult UttarakhandTD() => View("TopDestination/uttarakhandTD");

        public IActionResult MeghPack1() => View("Package/MeghPack1");
        public IActionResult MeghPack2() => View("Package/MeghPack2");

        public async Task<IActionResult> MeghPack3(int id)
        {
            var package = await _context.TravelPackages.Include(p => p.Itineraries).FirstOrDefaultAsync(p => p.PackageId == id);
            if (package == null) return NotFound();
            return View("Package/MeghPack3", package);
        }

        public IActionResult GoaPack1() => View("Package/GoaPack1");
        public IActionResult GoaPack2() => View("Package/GoaPack2");
        public IActionResult GoaPack3() => View("Package/GoaPack3");
        public IActionResult PaymentPage(int? bookingId) { ViewBag.BookingId = bookingId; return View(); }
    }
}