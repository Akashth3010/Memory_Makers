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
        public string Location { get; set; }
        public string PackageType { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Duration { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsTrending { get; set; }
        public string? Description { get; set; }
        //public PackageStatus status { get; set; } = PackageStatus.AVAILABLE;
        public PackageStatus AvailabilityStatus { get; set; } = PackageStatus.AVAILABLE;
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

        public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();

        // Relationship with Itinerary
        public int? HostId { get; set; }
        [ForeignKey("HostId")]
        public virtual HostContactDetail? Host { get; set; }
    }
}