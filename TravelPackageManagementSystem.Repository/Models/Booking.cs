using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Models

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

        [DataType(DataType.Date)]

        public DateTime TravelDate { get; set; }

        [Required]

        public int Guests { get; set; }

        [Required]

        [Phone]

        public string ContactPhone { get; set; }

        [Required]

        [Column(TypeName = "decimal(18,2")]

        public decimal TotalAmount { get; set; }

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

