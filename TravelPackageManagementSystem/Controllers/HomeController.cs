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

        
           

            // Add this method
            public IActionResult GoaTD()
            {
                return View();
            }

            // Add this for Uttarakhand
            public IActionResult UttarakhandTD()
            {
                return View();
            }

            // Add this for Meghalaya
            public IActionResult MeghalayaTD()
            {
                return View();
            }    
        public IActionResult GoaPack1()
        {
            return View();
        }

        public IActionResult GoaPack2()
        {
            return View();
        }

        public IActionResult GoaPack3()
        {
            return View();
        }
        public IActionResult MeghPack1()
        {
            return View();
        }
        public IActionResult MeghPack2()
        {
            return View();
        }
        public IActionResult MeghPack3()
        {
            return View();
        }
        public IActionResult UttaraPack1()
        {
            return View();
        }
        public IActionResult UttaraPack2()
        {
            return View();
        }
        public IActionResult UttaraPack3()
        {
            return View();
        }

    }
    }

