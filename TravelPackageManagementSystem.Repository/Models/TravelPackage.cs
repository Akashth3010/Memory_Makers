using System;
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
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Duration { get; set; } // e.g., "3 Days / 2 Nights"

        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; } // Path to the main image

        public bool IsTrending { get; set; }

        [Required]
        public PackageStatus Status { get; set; } = PackageStatus.AVAILABLE;

        // Relationship with Itinerary
        public virtual ICollection<Itinerary> Itineraries { get; set; }
    }
}