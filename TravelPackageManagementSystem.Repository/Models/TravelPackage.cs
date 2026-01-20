using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    public enum PackageStatus
    {
        AVAILABLE,
        UNAVAILABLE
    }

    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class TravelPackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageId { get; set; }

        [Required]
        [StringLength(100)]
        public string PackageName { get; set; }

        [Required]
        [StringLength(100)]
        public string Destination { get; set; } // e.g., "Meghalaya"

        [StringLength(100)]
        public string Location { get; set; }    // e.g., "Shillong Peak"

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public string PackageName { get; set; } = string.Empty;
        public string PackageType { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string? Location { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsTrending { get; set; }
        public string? Description { get; set; }

        // CHANGE 1: Rename the general availability status 
        // to avoid conflict with Approval status.
        public PackageStatus AvailabilityStatus { get; set; } = PackageStatus.AVAILABLE;

        [Required]
        [StringLength(50)]
        public string Duration { get; set; } // e.g., "3 Days / 2 Nights"

        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; } // Path to the main image

        public bool IsTrending { get; set; }
        // CHANGE 2: Keep this as 'Status' or 'ApprovalStatus' 
        // This is what your AdminController and JS use to filter.
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

        public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();

        // Relationship with Itinerary
        public virtual ICollection<Itinerary> Itineraries { get; set; }
        public int HostId { get; set; }
        [ForeignKey("HostId")]
        public virtual HostContactDetail? Host { get; set; }
    }
}