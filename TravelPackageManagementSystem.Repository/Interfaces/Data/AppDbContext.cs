using Microsoft.EntityFrameworkCore;
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

            // 2. Seed Travel Packages (HostId is omitted, so it will be NULL)
            modelBuilder.Entity<TravelPackage>().HasData(
                new TravelPackage
                {
                    PackageId = 1,
                    PackageName = "Quick Escape",
                    Destination = "Meghalaya",
                    Location = "Shillong Peak",
                    Price = 9999.00m,
                    Duration = "3 Days",
                    ImageUrl = "/lib/Image/meghalaya.jpg",
                    IsTrending = false,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                    // HostId is NOT set here
                },
                new TravelPackage
                {
                    PackageId = 2,
                    PackageName = "The Classic",
                    Destination = "Meghalaya",
                    Location = "Root Bridges",
                    Price = 17999.00m,
                    Duration = "5 Days",
                    ImageUrl = "/lib/Image/Waterfall.jpg",
                    IsTrending = true,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 3,
                    PackageName = "Deep Explorer",
                    Destination = "Meghalaya",
                    Location = "Hidden Caves",
                    Price = 25999.00m,
                    Duration = "7 Days",
                    ImageUrl = "/lib/Image/meghbg.jpg",
                    IsTrending = false,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                }
            );

            // 3. Seed Itinerary
            modelBuilder.Entity<Itinerary>().HasData(
                new Itinerary
                {
                    ItineraryId = 1,
                    PackageId = 3,
                    DayNumber = 1,
                    ActivityTitle = "Arrival in Shillong",
                    ActivityDescription = "Pick up from Guwahati...",
                    Inclusions = "Resort Stay;Daily Breakfast;Private SUV",
                    Exclusions = "Airfare;Lunch;Personal Expenses"
                },
                new Itinerary
                {
                    ItineraryId = 2,
                    PackageId = 3,
                    DayNumber = 2,
                    ActivityTitle = "Laitlum Canyons",
                    ActivityDescription = "Breathtaking views...",
                    Inclusions = "Resort Stay;Daily Breakfast;Private SUV",
                    Exclusions = "Airfare;Lunch;Personal Expenses"
                });
        }
    }
}