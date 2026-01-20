using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class GalleryImage
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;

        // Link this image to a specific Destination (e.g., Meghalaya)
        public int DestinationId { get; set; }
        public virtual Destination? ParentDestination { get; set; }
    }
}
