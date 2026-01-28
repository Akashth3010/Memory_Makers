using Microsoft.AspNetCore.Mvc;

namespace TravelPackageManagementSystem.Controllers
{
    public class UserController : Controller
    {
        // 1. GET: /User/Profile
        // This method simply returns the View you created.
        // Your JavaScript script handles the data loading from localStorage.
        public IActionResult Profile()
        {
            return View();
        }

        // 2. POST: /User/UpdateProfile
        // Optional: Use this if you want to save the user's changes to a database later.
        [HttpPost]
        public IActionResult UpdateProfile([FromBody] UserUpdateModel model)
        {
            if (model == null) return BadRequest();

            // Here you would typically save to a database using Entity Framework:
            // _context.Users.Update(model);
            // _context.SaveChanges();

            return Ok(new { message = "Profile updated successfully on server" });
        }
    }

    // A small class to handle the data coming from the frontend
    public class UserUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}