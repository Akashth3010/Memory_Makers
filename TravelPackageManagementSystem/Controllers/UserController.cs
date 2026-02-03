using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPackageManagementSystem.Repository.Data; // Ensure this matches your AppDbContext namespace
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Auth", "Home", new { returnUrl = "/User/Profile" });

            // 1. Fetch User AND Bookings AND Package Details
            var user = await _context.Users
                .Include(u => u.Bookings)
                .ThenInclude(b => b.TravelPackage) // <--- This was missing! Needed for Package Name/Image
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return NotFound();

            // 2. Fetch Wishlist Count
            var wishlistCount = await _context.Wishlists.CountAsync(w => w.UserId == userId);

            // 3. Create ViewModel
            var viewModel = new UserProfileViewModel
            {
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Initials = !string.IsNullOrEmpty(user.Username) ? user.Username.Substring(0, 1).ToUpper() : "U",

                BookingsCount = user.Bookings.Count,
                ReviewsCount = 0,
                WishlistCount = wishlistCount,

                // 4. MAP THE BOOKINGS (This brings the list back!)
                RecentBookings = user.Bookings.OrderByDescending(b => b.BookingDate).Select(b => new UserBookingDisplay
                {
                    BookingId = b.BookingId,
                    // Uses "Unknown" if Package is null, otherwise shows Name
                    PackageName = b.TravelPackage?.PackageName ?? "Unknown Package",
                    TravelDate = b.TravelDate,
                    Status = b.Status.ToString(),
                    Amount = b.TotalAmount
                }).ToList()
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Wishlist()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Auth", "Home");

            var wishlistItems = await _context.Wishlists
                .Include(w => w.TravelPackage) // Fetch Package Details
                .Where(w => w.UserId == userId)
                .Select(w => w.TravelPackage)
                .ToListAsync();

            return View(wishlistItems);
        }
        // POST: User/ToggleWishlist
        [HttpPost]
        public async Task<IActionResult> ToggleWishlist(int packageId)
        {
            // 1. Check Login
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(new { success = false, message = "Please login first" });
            }

            // 2. Check if item exists in Wishlist
            var existingItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.PackageId == packageId);

            if (existingItem != null)
            {
                // REMOVE it
                _context.Wishlists.Remove(existingItem);
                await _context.SaveChangesAsync();
                return Json(new { success = true, isAdded = false });
            }
            else
            {
                // ADD it
                var newItem = new Wishlist { UserId = userId.Value, PackageId = packageId };
                _context.Wishlists.Add(newItem);
                await _context.SaveChangesAsync();
                return Json(new { success = true, isAdded = true });
            }
        }

        // Optional: Action to Remove from Wishlist
        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int packageId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Json(new { success = false });

            var item = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.PackageId == packageId);

            if (item != null)
            {
                _context.Wishlists.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Wishlist");
        }
    }
}