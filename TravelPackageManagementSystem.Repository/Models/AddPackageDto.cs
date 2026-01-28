using System.Collections.Generic;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class AddPackageDto
    {
        public int? id { get; set; }
        public string name { get; set; } = string.Empty;
        public string dest { get; set; } = string.Empty;
        public int? destId { get; set; } // Matches DestinationId in SQL
        public string type { get; set; } = string.Empty;
        public string duration { get; set; } = string.Empty;
        public decimal price { get; set; }
        public string description { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
        public string imgUrl { get; set; } = string.Empty;

        // Matches the Thumbnail columns in your SQL table
        public string thumb1 { get; set; } = string.Empty;
        public string thumb2 { get; set; } = string.Empty;
        public string thumb3 { get; set; } = string.Empty;

        // ✅ Collection to handle the dynamic itinerary days from the Modal
        public List<ItineraryDto> itineraries { get; set; } = new List<ItineraryDto>();
    }

    public class ItineraryDto
    {
        public int dayNumber { get; set; }
        public string title { get; set; } = string.Empty;
        public string desc { get; set; } = string.Empty;
        public string inclusions { get; set; } = string.Empty;
        public string exclusions { get; set; } = string.Empty;
    }
}