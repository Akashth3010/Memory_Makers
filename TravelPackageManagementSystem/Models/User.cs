//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace TravelPackageManagementSystem.Application.Models
//{
//    public enum UserRole
//    {
//        ADMIN,
//        TRAVEL_AGENT,
//        CUSTOMER
//    }

//    public class User
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int UserId { get; set; }

//        [Required(ErrorMessage ="Username is Required")]
//        [StringLength(50)]
//        [Display(Name = "Username")]
//        public string Username { get; set; }

//        [Required(ErrorMessage ="Password is Required")]
//        [StringLength(255)] // Matches VARCHAR(255) for hashed passwords
//        [DataType(DataType.Password)]
//        public string Password { get; set; }

//        [Required(ErrorMessage ="Email address is Required")]
//        [EmailAddress(ErrorMessage ="Invalid Email Address")]
//        [StringLength(100)] // Updated to 100 to match your SQL schema
//        public string Email { get; set; }

//        [Required]
//        public UserRole Role { get; set; } = UserRole.CUSTOMER;

//        // Navigation Property initialized to prevent null errors
//        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
//    }
//}