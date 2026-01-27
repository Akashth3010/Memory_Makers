using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class HostSubmissionViewModel
    {
        // Host Info
        public string HostAgencyName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string CityCountry { get; set; } = string.Empty;

        // Package Info
        public string PackageName { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public int? DestinationId { get; set; } // Added for SQL sync
        public string Location { get; set; } = string.Empty;
        public string PackageType { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        // Media
        public string ImageUrl { get; set; } = string.Empty;
        public string ThumbnailUrl1 { get; set; } = string.Empty;
        public string ThumbnailUrl2 { get; set; } = string.Empty;
        public string ThumbnailUrl3 { get; set; } = string.Empty;

        // ✅ New: Collection for Itinerary Days
        public List<ItineraryDto> Itineraries { get; set; } = new List<ItineraryDto>();
    }
}