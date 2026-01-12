using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Application.Models
{
    public enum BookingStatus
    {
        CONFIRMED,
        CANCELLED,
        COMPLETED
    }
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.CONFIRMED;

        // Navigation properties
        [ForeignKey("CustomerId")]
        public virtual User User { get; set; }

        [ForeignKey("PackageId")]
        public virtual TravelPackage TravelPackage { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

    }
}
