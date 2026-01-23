using System.ComponentModel.DataAnnotations;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class TrendingCard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty; // e.g., "Mumbai"

        public string? Location { get; set; } // e.g., "Mumbai"

        public string? Category { get; set; } // e.g., "Attraction Tickets"

        public decimal StartingPrice { get; set; } // e.g., 35000.00

        public string? Description { get; set; } // The long text in your cards

        public string? ImageUrl { get; set; } // e.g., "/lib/TrendingImage/Mumbai.jpg"

        public double StarRating { get; set; } = 4.5;

        public int ReviewCount { get; set; } = 48;

        // This links the card to your actual booking/detail page
        public int? TargetPackageId { get; set; }
    }
}