using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Repository.Models;

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
        

        

        public IActionResult Manali()
        {
            return View("Trending/Manali");
        }

        public IActionResult Gangtok()
        {
            return View("Trending/Gangtok");
        }

        public IActionResult Jaipur()
        {
            return View("Trending/Jaipur");
        }
      

      
        public IActionResult GoaPack1() => View("Package/GoaPack1");
        public IActionResult GoaPack2() => View("Package/GoaPack2");
        public IActionResult GoaPack3() => View("Package/GoaPack3");
        public IActionResult KeralaPack1() => View("Package/KeralaPack1");
        public IActionResult KeralaPack2() => View("Package/KeralaPack2");
        public IActionResult KeralaPack3() => View("Package/KeralaPack3");
        public IActionResult MeghPack1() => View("Package/MeghPack1");
        public IActionResult MeghPack2() => View("Package/MeghPack2");

        public IActionResult MeghPack3() => View("Package/MeghPack3");
        public IActionResult MizoPack1() => View("Package/MizoPack1");
        public IActionResult MizoPack2() => View("Package/MizoPack2");

        public IActionResult MizoPack3() => View("Package/MizoPack3");
        public IActionResult TamilPack1() => View("Package/TamilPack1");
        public IActionResult TamilPack2() => View("Package/TamilPack2");

        public IActionResult TamilPack3() => View("Package/TamilPack3");
        public IActionResult UttaraPack1() => View("Package/UttaraPack1");
        public IActionResult UttaraPack2() => View("Package/UttaraPack2");

        public IActionResult UttaraPack3() => View("Package/UttaraPack3");


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

