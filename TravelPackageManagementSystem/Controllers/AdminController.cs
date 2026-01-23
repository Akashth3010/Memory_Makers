using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Repository.Data; // Ensure this points to your actual DbContext namespace

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        // Injecting the Database Context
        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Dashboard Page
        public IActionResult Dashboard()
        {
            return View();
        }

        // 2. Package Approvals Page
        public IActionResult Approvals()
        {
            return View();
        }

        // --- API ENDPOINTS FOR APPROVALS ---

        [HttpGet]
        public async Task<IActionResult> GetApprovals(string status)
        {
            // Fix: status from your JS is "pending", but your Enum starts with "Pending" (Capital P)
            // TryParse with 'true' handles case-insensitivity, which is good.
            if (!Enum.TryParse(status, true, out ApprovalStatus filterStatus))
            {
                return Json(new List<object>());
            }

            var list = await _context.TravelPackages
                .Include(p => p.Host)
                .Where(p => p.ApprovalStatus == filterStatus)
                .Select(p => new {
                    id = p.PackageId,        // Matches 'selectedId = data.id' in JS
                    host = p.Host != null ? p.Host.HostAgencyName : "Unknown Host",
                    email = p.Host != null ? p.Host.EmailAddress : "",
                    phone = p.Host != null ? p.Host.PhoneNumber : "",
                    city = p.Host != null ? p.Host.CityCountry : "",
                    package = p.PackageName, // Matches 'a.package' in JS
                    destination = p.Destination,
                    type = p.PackageType,
                    duration = p.Duration,
                    price = p.Price,
                    desc = p.Description
                })
                .ToListAsync();

            return Json(list);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null) return Json(new { success = false, message = "Package not found" });

            // Converting string status (e.g., "approved") to the ApprovalStatus Enum
            if (Enum.TryParse(status, true, out ApprovalStatus newStatus))
            {
                package.ApprovalStatus = newStatus;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Invalid status" });
        }

        // ------------------------------------

        // 3. Packages Page
        public IActionResult Packages()
        {
            return View();
        }

        // 4. Bookings Page
        public IActionResult Bookings()
        {
            return View();
        }

        // 5. Users Page
        public IActionResult Users()
        {
            return View();
        }

        // 6. Logout Logic
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}