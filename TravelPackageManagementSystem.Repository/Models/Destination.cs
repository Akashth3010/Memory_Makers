using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class Destination
    {
        [Key]
        public int DestinationId { get; set; }

        [Required]
        public string StateName { get; set; } = string.Empty; // e.g., "Meghalaya"

        public string ImageUrl { get; set; } = string.Empty;

        public int HotelCount { get; set; } // e.g., 30

        public int HolidayCount { get; set; } // e.g., 48
        public virtual ICollection<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>();

        // Navigation property: One destination has many packages
        public virtual ICollection<TravelPackage>? TravelPackages { get; set; }
    }
}
