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

        // CHANGE 2: Keep this as 'Status' or 'ApprovalStatus' 
        // This is what your AdminController and JS use to filter.
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

        public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();

        public int HostId { get; set; }
        [ForeignKey("HostId")]
        public virtual HostContactDetail? Host { get; set; }
    }
}