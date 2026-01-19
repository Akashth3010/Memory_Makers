using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Models
{
    public enum PaymentStatus
    {
        SUCCESS,
        FAILED,
        PENDING
    }
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Display(Name = "Amount")]
        public decimal PaymentAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.PENDING;

        [Required]
        public int BookingId { get; set; }
        //Navigation Property
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }
    }
}
