using Microsoft.EntityFrameworkCore;
//using TravelPackageManagementSystem.Application.Models;
using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TravelPackage> TravelPackages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<HostContactDetail> HostContactDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Unique constraint for Username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // 2. Seed Data for TravelPackage
            // Note: I am using INT for IDs to match your 'INT AUTO_INCREMENT' SQL requirement
            //modelBuilder.Entity<TravelPackage>().HasData(
            //    new TravelPackage
            //    {
            //        PackageId = 1,
            //        PackageName = "Paris Getaway",
            //        Destination = "France",
            //        Price = 1200.00m,
            //        Duration = 5,
            //        Status = PackageStatus.AVAILABLE
            //    },
            //    new TravelPackage
            //    {
            //        PackageId = 2,
            //        PackageName = "Tokyo Adventure",
            //        Destination = "Japan",
            //        Price = 1500.00m,
            //        Duration = 7,
            //        Status = PackageStatus.AVAILABLE
            //    }
            //);
        }
    }
}