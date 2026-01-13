//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace TravelPackageManagementSystem.Application.Models
//{
//    public enum PackageStatus
//    {
//        AVAILABLE,
//        UNAVAILABLE
//    }
//    public class TravelPackage
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int PackageId { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string PackageName { get; set; }

//        [Required]
//        [StringLength(100)]
//        public string Destination { get; set; }

//        [Required]
//        [Column(TypeName = "decimal(10,2)")]
//        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
//        public decimal Price { get; set; }

//        [Required]
//        [Range(1, 365)]
//        public int Duration { get; set; }

//        [Required]
//        public PackageStatus Status { get; set; } = PackageStatus.AVAILABLE;

//        public virtual ICollection<Itinerary> Itineraries { get; set; }
//    }
//}