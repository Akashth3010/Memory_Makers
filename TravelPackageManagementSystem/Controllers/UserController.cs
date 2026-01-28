using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Repository.Data;
using Microsoft.AspNetCore.Http; // Needed for Session

namespace TravelPackageManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /User/Profile
        public IActionResult Profile()
        {
            // Optional: validation to ensure user is logged in
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                // return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // GET: /User/Edit
        public IActionResult Edit()
        {
            return View();
        }

        // GET: /User/Wishlist
        public IActionResult Wishlist()
        {
            return View();
        }

        // ---------------------------------------------------------
        // LOGOUT ACTION
        // ---------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // 1. Clear the Session
            HttpContext.Session.Clear();

            // 2. Delete Cookies (optional, but recommended)
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            // 3. Redirect to HOME Page (Index action of HomeController)
            return RedirectToAction("Index", "Home");
        }

        // ---------------------------------------------------------
        // API to fetch data for the Profile Page
        // ---------------------------------------------------------
        [HttpGet]
        public IActionResult GetProfileData()
        {
            // Mock data - Replace this with actual database queries later
            var profileData = new
            {
                firstName = "John",
                lastName = "Doe",
                email = "john@example.com",
                phone = "123-456-7890",
                bookingsCount = 5,
                reviewsCount = 2,
                wishlistCount = 3,
                recentBookings = new[] {
                    new { destination = "Paris", refNumber = "#REF001", dateRange = "Oct 12 - Oct 15", status = "Confirmed" }
                }
            };
            return Json(profileData);
        }
    }
}