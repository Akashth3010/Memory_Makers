using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    public enum BookingStatus { CONFIRMED, CANCELLED, COMPLETED, AVAILABLE, PENDING }

    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required]
        public int PackageId { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime TravelDate { get; set; }

        [Required]
        public int Guests { get; set; }

        [Required]
        public string ContactPhone { get; set; } = string.Empty; // Fixed: Initialized

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.PENDING;

        [Required]
        public int UserId { get; set; }

        // Navigation properties (Fixed: Nullable to stop build warnings)
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("PackageId")]
        public virtual TravelPackage? TravelPackage { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}