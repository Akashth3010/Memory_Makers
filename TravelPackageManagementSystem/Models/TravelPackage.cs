using System.ComponentModel.DataAnnotations;

namespace TravelPackageManagementSystem.Models
{
    public class TravelPackage
    {
        [Key] // This tells SQL this is the Primary Key (ID)
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } // e.g., "Himalayan Grand Explorer"

        public string Description { get; set; } // e.g., "7 Days Auli Skiing..."

        public double Rating { get; set; } // e.g., 4.9

        public int ReviewCount { get; set; } // e.g., 310

        public string Duration { get; set; } // e.g., "7 Days / 6 Nights"

        public string ImagePath { get; set; } // Path to your image in wwwroot
    }
}