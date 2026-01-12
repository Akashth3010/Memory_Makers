using Microsoft.AspNetCore.Http; // Required for Session
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TravelPackageManagementSystem.Application.Data;
using TravelPackageManagementSystem.Application.Models;

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
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // 1. Check for existing Username
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username || u.Email == model.Email);

            if (existingUser != null)
            {
                if (existingUser.Username == model.Username)
                {
                    return BadRequest(new { Username = "This username is already taken." });
                }
                if (existingUser.Email == model.Email)
                {
                    return BadRequest(new { Email = "This email is already registered." });
                }
            }

            // 2. If no duplicate, proceed with creation
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Role = UserRole.CUSTOMER
            };

            user.Password = _hasher.HashPassword(user, model.Password);

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync(); // This is where it was crashing
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Username = "A database error occurred. Please try a different username." });
            }

            // 3. Set Session and return success
            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            return Ok(new { success = true });
        }

        // Update Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // 1. Find user by Username (since your LoginViewModel uses Username)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username || u.Email == model.Username);

            // 2. Verify user exists and password matches
            if (user == null || _hasher.VerifyHashedPassword(user, user.Password, model.Password) == PasswordVerificationResult.Failed)
            {
                // Return a key that matches your "err-Password" span in the HTML
                return BadRequest(new { Password = "Invalid Username or Password" });
            }

            // 3. CRITICAL: Set the exact same Session keys as Register
            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            return Ok(new { success = true, username = user.Username });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Logged out successfully" });
        }
    }
}