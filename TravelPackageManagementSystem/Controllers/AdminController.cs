using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;
using System.Security.Claims;

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public AdminController(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // 1. Dashboard - Fixed to ensure real counts and no null errors
        public async Task<IActionResult> Dashboard()
        {
            var users = await _userService.GetAllUsersAsync();

            // Stats for the Dashboard Cards
            ViewBag.TotalAccounts = users?.Count() ?? 0;
            ViewBag.OnlineUsers = users?.Count(u => u.IsLoggedIn) ?? 0;
            ViewBag.HostCount = await _context.HostContactDetails.CountAsync();

            // Adding these so the "Total Packages" and "Pending" cards also work
            ViewBag.TotalPackages = await _context.TravelPackages.CountAsync();
            ViewBag.PendingApprovals = await _context.TravelPackages.CountAsync(p => p.ApprovalStatus == ApprovalStatus.Pending);

            return View();
        }

        // 2. Users Management - FIXED with Null-Safe Dictionary
        public async Task<IActionResult> Users()
        {
            var loginUsers = await _userService.GetAllUsersAsync();
            var hostAgencies = await _context.HostContactDetails.ToListAsync();

            // FIX: Added 'p.HostId != null' to prevent the "Value cannot be null" error
            var approvedPackageCounts = await _context.TravelPackages
                .Where(p => p.ApprovalStatus == ApprovalStatus.Approved && p.HostId != null)
                .GroupBy(p => p.HostId)
                .Select(g => new {
                    HostId = g.Key.Value, // Safely get the int value
                    Count = g.Count()
                })
                .ToDictionaryAsync(x => x.HostId, x => x.Count);

            ViewBag.HostAgencies = hostAgencies;
            ViewBag.ApprovedPackageCounts = approvedPackageCounts;

            return View(loginUsers);
        }

        // 3. Package Approvals View
        public IActionResult Approvals() => View();

        // 4. API for Approvals Tab
        [HttpGet]
        public async Task<IActionResult> GetApprovals(string status)
        {
            if (!Enum.TryParse(status, true, out ApprovalStatus filterStatus))
            {
                return Json(new List<object>());
            }

            var list = await _context.TravelPackages
                .Where(p => p.ApprovalStatus == filterStatus)
                .Select(p => new {
                    id = p.PackageId,
                    host = p.Host != null ? p.Host.HostAgencyName : "Unknown Host",
                    email = p.Host != null ? p.Host.EmailAddress : "",
                    phone = p.Host != null ? p.Host.PhoneNumber : "",
                    city = p.Host != null ? p.Host.CityCountry : "",
                    package = p.PackageName,
                    destination = p.Destination,
                    type = p.PackageType,
                    duration = p.Duration,
                    price = p.Price,
                    desc = p.Description
                })
                .ToListAsync();

            return Json(list);
        }

        // 5. Update Status
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null) return Json(new { success = false });

            if (Enum.TryParse(status, true, out ApprovalStatus newStatus))
            {
                package.ApprovalStatus = newStatus;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult Packages() => View();
        public IActionResult Bookings() => View();

        // 6. Logout
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                await _userService.UpdateLoginStatusAsync(userId, false);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}