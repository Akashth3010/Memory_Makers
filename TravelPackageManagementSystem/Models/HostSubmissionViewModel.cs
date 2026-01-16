namespace TravelPackageManagementSystem.Application.Models
{
    public class HostSubmissionViewModel
    {
        //Host Info
        public string HostAgencyName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string CityCountry { get; set; } = string.Empty;

        //Package Info
        public string PackageName { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string PackageType { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public IFormFile? PackageImage { get; set; }
    }
}
