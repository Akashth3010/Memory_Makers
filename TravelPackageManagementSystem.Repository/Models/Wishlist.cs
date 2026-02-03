using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelPackageManagementSystem.Repository.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("TravelPackage")]
        public int PackageId { get; set; }
        public virtual TravelPackage TravelPackage { get; set; }
    }
}