using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthModelService _authService;

        public AccountController(IAuthModelService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            // 1. Validation Logic
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                // Returns JSON so frontend stops loading
                return BadRequest(new { success = false, message = "Validation Failed", errors = errors });
            }

            // 2. Call Service
            var result = await _authService.RegisterUserAsync(model);

            if (!result.Success)
            {
                // Returns JSON error if user already exists
                return BadRequest(new { success = false, message = result.Message });
            }

            // 3. Set Session & Return Success
            SetUserSession(result.User);
            return Ok(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Please fill all fields." });
            }

            // Call Service
            var user = await _authService.AuthenticateUserAsync(model.Username, model.Password);

            if (user == null)
            {
                // Returns JSON error for invalid password
                return BadRequest(new { success = false, message = "Invalid Username or Password" });
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

        // --- HELPER METHOD (FIXED) ---
        private void SetUserSession(User user)
        {
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Username ?? "");
                HttpContext.Session.SetInt32("UserId", user.UserId);

                // FIXED LINE: Convert Enum to String
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
            }
        }
    }
}