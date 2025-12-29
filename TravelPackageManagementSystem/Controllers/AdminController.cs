using Microsoft.AspNetCore.Mvc;

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AdminController : Controller
    {
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
            // This takes the admin back to the home page after logout
            return RedirectToAction("Index", "Home");
        }
    }
}