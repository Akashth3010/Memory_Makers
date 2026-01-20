using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    // Fix: This matches your requirement for CONFIRMED, CANCELLED, etc.
    public enum BookingStatus
    {
        CONFIRMED,
        CANCELLED,
        COMPLETED,
        AVAILABLE,
        PENDING
    }

    public class Booking
    {
        // Inside your Booking class
        public virtual TravelPackage? TravelPackage { get; set; } // The '?' makes it optional
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [ForeignKey("PackageId")]
        //public virtual required TravelPackage? TravelPackage { get; set; }

        [Required]
        public int UserId { get; set; } // Fix: Added this to resolve 'UserId' error

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.PENDING; // Fix: Uses Enum type
        
            // ... your existing properties ...

            [Required, EmailAddress]
            public string ContactEmail { get; set; } = string.Empty;

            [Required, Phone]
            public string ContactPhone { get; set; } = string.Empty;
        
    }
}