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

       
        public IActionResult Vrindavan()
        {
            return View("Trending/Vrindavan");
        }
        public IActionResult Rameshwaram()
        {
            return View("Trending/Rameshwaram");
        }
        public IActionResult Darjiling()
        {
            return View("Trending/Darjiling");
        }
        

        // The individual detail page
        public IActionResult Tamilnadu()
        {
            return View();
        }
        public IActionResult TamilnaduTD(string id)
        {
            ViewBag.PackageId = id;
            return View("TopDestination/TamilnaduTD");
        }
        public IActionResult KeralaTD(string id)
        {
            ViewBag.PackageId = id;
            return View("TopDestination/KeralaTD");
        }
        public IActionResult MizoramTD(string id)
        {
            ViewBag.PackageId = id;
            return View("TopDestination/MizoramTD");
        }


        
           

            // Add this method
            public IActionResult GoaTD()
            {
                return View("TopDestination/GoaTD");
            }

            // Add this for Uttarakhand
            public IActionResult UttarakhandTD()
            {
                return View("TopDestination/uttarakhandTD");
            }

            // Add this for Meghalaya
            public IActionResult MeghalayaTD()
            {
                return View("TopDestination/MeghalayaTD");
            }    
        public IActionResult GoaPack1()
        {
            return View("Packages/Goa/GoaPack1");
        }

        

        public IActionResult Manali()
        {
            return View("Trending/Manali");
        }

        public IActionResult GoaPack2()
        {
            return View();
        }
        public IActionResult Gangtok()
        {
            return View("Trending/Gangtok");
        }

        public IActionResult Jaipur()
        {
            return View("Trending/Jaipur");
        }
        public IActionResult KeralaPack1()
        {
            return View();
        }
        public IActionResult KeralaPack2()
        {
            return View();
        }
        public IActionResult KeralaPack3()
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

        public IActionResult Banaras()
        {
            return View("Trending/Banaras");
        }

        public IActionResult Mumbai()
        {
            return View("Trending/Mumbai");
        }

        public IActionResult Munnar()
        {
            return View("Trending/Munnar");
        }

        public IActionResult Ooty()
        {
            return View("Trending/Ooty");
        }
        public IActionResult Goa()
        {
            return View("Trending/Goa");
        }
       

        public IActionResult TajMahal()
        {
            return View("Trending/TajMahal");
        }

        public IActionResult Host() {
            
            return View(); }
        public IActionResult PaymentPage()
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

        public IActionResult MyBookings()
        {
            return View();
        }
    }
    }

