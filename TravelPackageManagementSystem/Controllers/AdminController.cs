using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Application.Models;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard() => View();
        public IActionResult Approvals() => View();
        public IActionResult Packages() => View();
        public IActionResult Bookings() => View();
        public IActionResult Users() => View();

        [HttpGet]
        public async Task<IActionResult> GetApprovals(string status)
        {
            ApprovalStatus filterStatus = status.ToLower() switch
            {
                "approved" => ApprovalStatus.Approved,
                "rejected" => ApprovalStatus.Rejected,
                _ => ApprovalStatus.Pending
            };

            var data = await _context.TravelPackages
                .Include(p => p.Host)
                .Where(p => p.ApprovalStatus == filterStatus)
                .Select(p => new
                {
                    id = p.PackageId,
                    package = p.PackageName,
                    host = p.Host != null ? p.Host.HostAgencyName : "Admin",
                    email = p.Host != null ? p.Host.EmailAddress : "N/A",
                    phone = p.Host != null ? p.Host.PhoneNumber : "N/A",
                    city = p.Host != null ? p.Host.CityCountry : "N/A",
                    destination = p.Destination,
                    type = p.PackageType,
                    duration = p.Duration,
                    price = p.Price,
                    desc = p.Description
                })
                .ToListAsync();

            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null) return Json(new { success = false, message = "Package not found" });

            if (status == "Approved")
                package.ApprovalStatus = ApprovalStatus.Approved;
            else if (status == "Rejected")
                package.ApprovalStatus = ApprovalStatus.Rejected;

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingCount()
        {
            int count = await _context.TravelPackages
                .CountAsync(p => p.ApprovalStatus == ApprovalStatus.Pending);
            return Json(new { count });
        }

        [HttpGet]
        public async Task<IActionResult> GetPackages()
        {
            var packages = await _context.TravelPackages
                .Where(p => p.ApprovalStatus == ApprovalStatus.Approved)
                .Select(p => new
                {
                    id = p.PackageId,
                    name = p.PackageName,
                    dest = p.Destination,
                    type = p.PackageType,
                    price = p.Price,
                    duration = p.Duration,
                    status = p.AvailabilityStatus == PackageStatus.AVAILABLE ? "Active" : "Inactive",
                    category = p.IsTrending ? "Trending" : "Normal"
                })
                .ToListAsync();

            return Json(packages);
        }

        [HttpGet]
        public async Task<IActionResult> GetPackageById(int id)
        {
            var p = await _context.TravelPackages
                .Include(x => x.Itineraries)
                .Include(x => x.Host)
                .FirstOrDefaultAsync(x => x.PackageId == id);

            if (p == null) return NotFound();

            return Json(new
            {
                id = p.PackageId,
                name = p.PackageName,
                dest = p.Destination,
                destId = p.DestinationId,
                type = p.PackageType,
                duration = p.Duration,
                price = p.Price,
                description = p.Description,
                location = p.Location,
                imgUrl = p.ImageUrl,
                thumb1 = p.ThumbnailUrl1,
                thumb2 = p.ThumbnailUrl2,
                thumb3 = p.ThumbnailUrl3,
                host = p.Host != null ? p.Host.HostAgencyName : "Admin",
                itineraries = p.Itineraries.OrderBy(i => i.DayNumber).Select(i => new {
                    dayNumber = i.DayNumber,
                    title = i.ActivityTitle,
                    desc = i.ActivityDescription,
                    inclusions = i.Inclusions,
                    exclusions = i.Exclusions
                })
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddPackage([FromBody] AddPackageDto model)
        {
            if (model == null) return BadRequest();

            var package = new TravelPackage
            {
                PackageName = model.name,
                Destination = model.dest,
                DestinationId = model.destId,
                PackageType = model.type,
                Duration = model.duration,
                Price = model.price,
                Description = model.description,
                Location = model.location,
                ImageUrl = model.imgUrl ?? "",
                ThumbnailUrl1 = model.thumb1 ?? "",
                ThumbnailUrl2 = model.thumb2 ?? "",
                ThumbnailUrl3 = model.thumb3 ?? "",
                ApprovalStatus = ApprovalStatus.Approved,
                AvailabilityStatus = PackageStatus.AVAILABLE,
                IsTrending = false,
                HostId = null
            };

            _context.TravelPackages.Add(package);
            await _context.SaveChangesAsync();

            if (model.itineraries != null && model.itineraries.Count > 0)
            {
                foreach (var item in model.itineraries)
                {
                    _context.Itineraries.Add(new Itinerary
                    {
                        PackageId = package.PackageId,
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

        [HttpPost]
        public async Task<IActionResult> UpdatePackage(int id, [FromBody] AddPackageDto model)
        {
            var package = await _context.TravelPackages
                .Include(p => p.Itineraries)
                .FirstOrDefaultAsync(p => p.PackageId == id);

            if (package == null) return Json(new { success = false });

            package.PackageName = model.name;
            package.Destination = model.dest;
            package.DestinationId = model.destId;
            package.PackageType = model.type;
            package.Duration = model.duration;
            package.Price = model.price;
            package.Description = model.description;
            package.Location = model.location;
            package.ImageUrl = model.imgUrl ?? "";
            package.ThumbnailUrl1 = model.thumb1 ?? "";
            package.ThumbnailUrl2 = model.thumb2 ?? "";
            package.ThumbnailUrl3 = model.thumb3 ?? "";

            // Clear old itineraries and replace with updated ones
            _context.Itineraries.RemoveRange(package.Itineraries);

            if (model.itineraries != null)
            {
                foreach (var item in model.itineraries)
                {
                    _context.Itineraries.Add(new Itinerary
                    {
                        PackageId = id,
                        DayNumber = item.dayNumber,
                        ActivityTitle = item.title,
                        ActivityDescription = item.desc,
                        Inclusions = item.inclusions,
                        Exclusions = item.exclusions
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> TogglePackageStatus(int id)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null) return Json(new { success = false });

            package.AvailabilityStatus = package.AvailabilityStatus == PackageStatus.AVAILABLE
                ? PackageStatus.UNAVAILABLE : PackageStatus.AVAILABLE;

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleTrending(int id)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null) return Json(new { success = false });

            package.IsTrending = !package.IsTrending;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePackage(int id, string password)
        {
            if (password != "EasySafarDelete")
                return Json(new { success = false, message = "Wrong password" });

            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null) return Json(new { success = false, message = "Package not found" });

            _context.TravelPackages.Remove(package);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public IActionResult Logout() => RedirectToAction("Index", "Home");
    }
}