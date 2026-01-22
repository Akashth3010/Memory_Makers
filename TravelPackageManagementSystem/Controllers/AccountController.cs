using Microsoft.AspNetCore.Mvc;
using TravelPackageManagementSystem.Repository.Models;
using TravelPackageManagementSystem.Services.Interfaces;

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthModelService _authService;
        private readonly IUserService _userService; // NEW: Added User Service

        // Inject both the Auth Service and the User Service
        public AccountController(IAuthModelService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(model);

            if (!result.Success)
            {
                return BadRequest(new { Username = result.Message });
            }

            // TRIGGER: Set online status to true upon registration
            await _userService.UpdateLoginStatusAsync(result.User.UserId, true);

            SetUserSession(result.User);
            return Ok(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _authService.AuthenticateUserAsync(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new { Password = "Invalid Username or Password" });
            }

            // TRIGGER: Set online status to true upon successful login
            await _userService.UpdateLoginStatusAsync(user.UserId, true);

            SetUserSession(user);
            return Ok(new { success = true, username = user.Username });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Get the UserId from session before clearing it
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // TRIGGER: Set online status to false upon logout
                await _userService.UpdateLoginStatusAsync(userId.Value, false);
            }

            HttpContext.Session.Clear();
            return Ok(new { message = "Logged out successfully" });
        }

        private void SetUserSession(User user)
        {
            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());
        }
    }
}