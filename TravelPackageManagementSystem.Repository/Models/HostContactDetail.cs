using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class HostContactDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string HostAgencyName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string CityCountry { get; set; } = string.Empty;

        // ✅ ADDED FOR LOGIN
        [Required]
        public string Password { get; set; } = string.Empty;

        public virtual ICollection<TravelPackage> TravelPackages { get; set; } = new List<TravelPackage>();
    }
}