using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Application.Models
{
    public class Itinerary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItineraryId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        [Display(Name = "Day")]
        [Range(1, 365, ErrorMessage = "Day number must be between 1 and 365")]
        public int DayNumber { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Activity Description")]
        public string ActivityDescription { get; set; }

        //Navigation Property
        [ForeignKey("PackageId")]
        public virtual TravelPackage TravelPackage { get; set; }
    }
}
