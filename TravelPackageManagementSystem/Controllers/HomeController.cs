using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Models;

namespace TravelPackageManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Hero()
        {
            return View();
        }
        

        // The individual detail page
        public IActionResult Tamilnadu()
        {
            return View();
        }
        public IActionResult TamilnaduTD(string id)
        {
            ViewBag.PackageId = id;
            return View();
        }
        public IActionResult KeralaTD(string id)
        {
            ViewBag.PackageId = id;
            return View();
        }
        public IActionResult MizoramTD(string id)
        {
            ViewBag.PackageId = id;
            return View();
        }

    }
}
