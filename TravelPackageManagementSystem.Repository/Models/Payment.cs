using System.ComponentModel.DataAnnotations;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; } // e.g., "Credit Card", "PayPal"

        public string TransactionId { get; set; }

        public string Status { get; set; } // e.g., "Pending", "Completed", "Failed"

        // Foreign Key example - linking to a Booking or Package
        // public int BookingId { get; set; }
        // public Booking Booking { get; set; }
       
    }
}