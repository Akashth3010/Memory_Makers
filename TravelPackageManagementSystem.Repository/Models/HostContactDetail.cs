using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class HostContactDetail
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Host or Agency Name is required")]
        [StringLength(200)]
        [Display(Name = "Host / Agency Name")]
        public string HostAgencyName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(200)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="City and Country are required")]
        [StringLength (200)]
        [Display(Name = "City / Country")]
        public string CityCountry { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
