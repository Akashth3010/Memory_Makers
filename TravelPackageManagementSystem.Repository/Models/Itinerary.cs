using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class Itinerary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItineraryId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        [Display(Name = "Day Number")]
        [Range(1, 365, ErrorMessage = "Day number must be between 1 and 365")]
        public int DayNumber { get; set; }

        [Required(ErrorMessage = "Activity Title is required")]
        [StringLength(100)]
        [Display(Name = "Activity Title")]
        // Added this so your Day-by-Day headers (e.g. "Arrival in Shillong") are dynamic
        public string ActivityTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Activity Description is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Activity Description")]
        // Initialized with string.Empty to resolve nullability warnings
        public string ActivityDescription { get; set; } = string.Empty;

        [Display(Name = "What's Included")]
        public string Inclusions { get; set; } = string.Empty; // e.g., "Resorts;Meals;SUV"

        [Display(Name = "What's Excluded")]
        public string Exclusions { get; set; } = string.Empty; // e.g., "Flights;Insurance"

        // Navigation Property
        [ForeignKey("PackageId")]
        // Changed to virtual? to allow the itinerary to exist even if the package isn't joined
        public virtual TravelPackage? TravelPackage { get; set; }
    }
}