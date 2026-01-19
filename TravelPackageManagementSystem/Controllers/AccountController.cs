using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces; 

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthModelService _authService;

        // Inject the Service, not the DbContext
        public AccountController(IAuthModelService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Logic is moved to Service. RegisterUserAsync should return a result object.
            var result = await _authService.RegisterUserAsync(model);

            if (!result.Success)
            {
                // result.Message would contain "Username or Email already taken"
                return BadRequest(new { Username = result.Message });
            }

            // Set Session using the User object returned from the service
            SetUserSession(result.User);

            return Ok(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Pass the username/password to the service to verify
            var user = await _authService.AuthenticateUserAsync(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new { Password = "Invalid Username or Password" });
            }

            // Set Session
            SetUserSession(user);

            return Ok(new { success = true, username = user.Username });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Logged out successfully" });
        }

        // Helper method to keep Session keys consistent
        private void SetUserSession(User user)
        {
            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());
        }
    }
}