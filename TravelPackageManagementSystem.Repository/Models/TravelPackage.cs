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

    public class TravelPackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageId { get; set; }

        [Required]
        public string PackageName { get; set; } = string.Empty;

        // NEW: Foreign Key linking to the Destination Table
        [Required]
        public int DestinationId { get; set; }

        [ForeignKey("DestinationId")]
        public virtual Destination? ParentDestination { get; set; }

        public string? Location { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Duration { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsTrending { get; set; }
        public string? Description { get; set; }

        public PackageStatus Status { get; set; } = PackageStatus.AVAILABLE;

        public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    }
}