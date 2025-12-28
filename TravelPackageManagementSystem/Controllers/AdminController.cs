using Microsoft.AspNetCore.Mvc;

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AdminController : Controller
    {
        // ================= DASHBOARD =================
        // Admin landing page
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult PackageApprovals()
        {
            return View();
        }

    }
}
   