using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    public enum UserRole
    {
        ADMIN,
        TRAVEL_AGENT,
        CUSTOMER
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [StringLength(50)]
        [Display(Name = "Username")]
        // Fix: Initialize with string.Empty
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        // Fix: Initialize with string.Empty
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100)]
        // Fix: Initialize with string.Empty
        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.CUSTOMER;

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}