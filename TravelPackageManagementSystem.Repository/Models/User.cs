using System;
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
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        // Optional profile fields (add if needed)
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public UserRole Role { get; set; } = UserRole.CUSTOMER;

        public virtual ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    }
}