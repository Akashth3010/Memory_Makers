using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http; // Required for Session
using TravelPackageManagementSystem.Application.Models;
using TravelPackageManagementSystem.Application.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackageManagementSystem.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        // PasswordHasher helps securely store passwords
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        // Update Register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model) // Use ViewModel
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_context.Users.Any(u => u.Email == model.Email || u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "User already exists.");
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Role = UserRole.CUSTOMER,
                // Password will be set below
            };

            user.Password = _hasher.HashPassword(user, model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            return Ok(new { success = true });
        }

        // Update Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model) // Use ViewModel
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null || _hasher.VerifyHashedPassword(user, user.Password, model.Password) == PasswordVerificationResult.Failed)
            {
                // Return a dictionary so your JS displayErrors function works
                return BadRequest(new { Password = new[] { "Invalid Username or Password" } });
            }

            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            return Ok(new { username = user.Username, role = user.Role.ToString() });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Logged out successfully" });
        }
    }
}