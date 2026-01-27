using System.Collections.Generic;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class AddPackageDto
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string dest { get; set; }
        public int? destId { get; set; } // Matches DestinationId
        public string type { get; set; }
        public string duration { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string imgUrl { get; set; }
        public string thumb1 { get; set; } // Matches ThumbnailUrl1
        public string thumb2 { get; set; } // Matches ThumbnailUrl2
        public string thumb3 { get; set; } // Matches ThumbnailUrl3

        // ✅ Dynamic Itinerary List
        public List<ItineraryDto> itineraries { get; set; } = new List<ItineraryDto>();
    }

    public class ItineraryDto
    {
        public int dayNumber { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string inclusions { get; set; }
        public string exclusions { get; set; }
    }
}