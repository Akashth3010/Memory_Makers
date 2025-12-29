using Microsoft.AspNetCore.Mvc;

namespace TravelPackageManagementSystem.Controllers
{
    public class User : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}