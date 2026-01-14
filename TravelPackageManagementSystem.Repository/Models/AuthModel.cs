using System.ComponentModel.DataAnnotations;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is Required")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email address is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}