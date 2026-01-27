using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [Required]
        [StringLength(100)]
        public string TransactionId { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [Required]
        public int BookingId { get; set; }

        // FIX: Add the '?' to make it nullable. 
        // This prevents the "Booking field is required" validation error.
        [ForeignKey("BookingId")]
        public virtual Booking? Booking { get; set; }
    }
}