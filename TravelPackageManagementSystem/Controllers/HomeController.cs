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
        

        public IActionResult Manali()
        {
            return View();
        }

        public IActionResult Gangtok()
        {
            return View();
        }

        public IActionResult Banaras()
        {
            return View();
        }

        public IActionResult TajMahal()
        {
            return View();
        }

        public IActionResult PaymentPage()
        {
            return View();
        }

        public IActionResult Host()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult TravelGuide()
        {
            return View();
        }

        public IActionResult CustomerSupport ()
        {
            return View();
        }
    }
}
