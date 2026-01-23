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
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<HostContactDetail> HostContactDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Seed Destinations
            modelBuilder.Entity<Destination>().HasData(
                new Destination { DestinationId = 1, StateName = "Meghalaya", ImageUrl = "/lib/Image/meghalaya.jpg", HotelCount = 18, HolidayCount = 29 },
                new Destination { DestinationId = 2, StateName = "Tamil Nadu", ImageUrl = "/lib/Image/Tamilnadu.jpg", HotelCount = 30, HolidayCount = 48 },
                new Destination { DestinationId = 3, StateName = "Kerala", ImageUrl = "/lib/Image/Kerala.jpg", HotelCount = 22, HolidayCount = 36 },
                new Destination { DestinationId = 4, StateName = "Goa", ImageUrl = "/lib/Image/goa.jpg", HotelCount = 25, HolidayCount = 40 },
                new Destination { DestinationId = 5, StateName = "Maharashtra", ImageUrl = "/lib/TrendingImage/Mumbai.jpg", HotelCount = 45, HolidayCount = 60 },
                new Destination { DestinationId = 6, StateName = "Rajasthan", ImageUrl = "/lib/TrendingImage/Hawa Mahal Jaipur.jpg", HotelCount = 50, HolidayCount = 75 },
                new Destination { DestinationId = 7, StateName = "West Bengal", ImageUrl = "/lib/TrendingImage/Darjiling.jpg", HotelCount = 20, HolidayCount = 30 },
                new Destination { DestinationId = 8, StateName = "Sikkim", ImageUrl = "/lib/TrendingImage/Gangtok.jpg", HotelCount = 15, HolidayCount = 25 },
                new Destination { DestinationId = 9, StateName = "Uttar Pradesh", ImageUrl = "/lib/TrendingImage/Banaras.jpg", HotelCount = 40, HolidayCount = 55 }
            );

            // 2. Seed Travel Packages
            modelBuilder.Entity<TravelPackage>().HasData(
                new TravelPackage
                {
                    PackageId = 1,
                    PackageName = "Quick Escape",
                    Destination = "Meghalaya",
                    DestinationId = 1,
                    Location = "Shillong Peak",
                    Price = 9999.00m,
                    Duration = "3 Days",
                    ImageUrl = "/lib/Image/meghalaya.jpg",
                    ThumbnailUrl1 = "/lib/Image/meg4.jpg",
                    ThumbnailUrl2 = "/lib/Image/meghbg.jpg",
                    ThumbnailUrl3 = "/lib/Image/meg3.jpg",
                    IsTrending = false,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 2,
                    PackageName = "Temple Trail",
                    Destination = "Tamil Nadu",
                    DestinationId = 2,
                    Location = "Madurai",
                    Price = 18500.00m,
                    Duration = "8 Days",
                    ImageUrl = "/lib/Image/Tamilnadu.jpg",
                    ThumbnailUrl1 = "/lib/Image/t4.jpg",
                    ThumbnailUrl2 = "/lib/Image/t25.jpg",
                    ThumbnailUrl3 = "/lib/Image/t3.jpg",
                    IsTrending = false,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 3,
                    PackageName = "Backwater Bliss",
                    Destination = "Kerala",
                    DestinationId = 3,
                    Location = "Alleppey",
                    Price = 22000.00m,
                    Duration = "6 Days",
                    ImageUrl = "/lib/Image/Kerala.jpg",
                    ThumbnailUrl1 = "/lib/Image/k32.jpg",
                    ThumbnailUrl2 = "/lib/Image/k35.jpg",
                    ThumbnailUrl3 = "/lib/Image/k3.jpg",
                    IsTrending = false,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 4,
                    PackageName = "Mumbai Heritage",
                    Destination = "Maharashtra",
                    DestinationId = 5,
                    Location = "Gateway of India",
                    Price = 35000.00m,
                    Duration = "5 Days",
                    ImageUrl = "/lib/TrendingImage/Mumbai.jpg",
                    ThumbnailUrl1 = "/lib/Image/mumbai1.jpg",
                    ThumbnailUrl2 = "/lib/Image/mumbai2.jpg",
                    ThumbnailUrl3 = "/lib/Image/mumbai3.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE,
                    Description = "A bustling metropolis blending colonial heritage and Bollywood glamour."
                },
                new TravelPackage
                {
                    PackageId = 5,
                    PackageName = "Pink City Tour",
                    Destination = "Rajasthan",
                    DestinationId = 6,
                    Location = "Jaipur",
                    Price = 65000.00m,
                    Duration = "6 Days",
                    ImageUrl = "/lib/TrendingImage/Hawa Mahal Jaipur.jpg",
                    ThumbnailUrl1 = "/lib/Image/jaipur1.jpg",
                    ThumbnailUrl2 = "/lib/Image/jaipur2.jpg",
                    ThumbnailUrl3 = "/lib/Image/jaipur3.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 6,
                    PackageName = "Sikkim Adventure",
                    Destination = "Sikkim",
                    DestinationId = 8,
                    Location = "Gangtok",
                    Price = 70000.00m,
                    Duration = "8 Days",
                    ImageUrl = "/lib/TrendingImage/Gangtok.jpg",
                    ThumbnailUrl1 = "/lib/Image/gangtok1.jpg",
                    ThumbnailUrl2 = "/lib/Image/gangtok2.jpg",
                    ThumbnailUrl3 = "/lib/Image/gangtok3.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 7,
                    PackageName = "Darjeeling Tea",
                    Destination = "West Bengal",
                    DestinationId = 7,
                    Location = "Darjeeling",
                    Price = 5500.00m,
                    Duration = "4 Days",
                    ImageUrl = "/lib/TrendingImage/Darjiling.jpg",
                    ThumbnailUrl1 = "/lib/Image/darjiling1.jpg",
                    ThumbnailUrl2 = "/lib/Image/darjiling2.jpg",
                    ThumbnailUrl3 = "/lib/Image/darjiling3.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 8,
                    PackageName = "Varanasi Spiritual",
                    Destination = "Uttar Pradesh",
                    DestinationId = 9,
                    Location = "Varanasi",
                    Price = 83000.00m,
                    Duration = "3 Days",
                    ImageUrl = "/lib/TrendingImage/Banaras.jpg",
                    ThumbnailUrl1 = "/lib/Image/banaras1.jpg",
                    ThumbnailUrl2 = "/lib/Image/banaras2.jpg",
                    ThumbnailUrl3 = "/lib/Image/banaras3.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 9,
                    PackageName = "Misty Munnar",
                    Destination = "Kerala",
                    DestinationId = 3,
                    Location = "Munnar",
                    Price = 50000.00m,
                    Duration = "5 Days",
                    ImageUrl = "/lib/TrendingImage/Munnar.jpg",
                    ThumbnailUrl1 = " /lib/Image/munnar1.jpg",
                    ThumbnailUrl2 = "/lib/Image/munnar2.jpg",
                    ThumbnailUrl3 = "/lib/Image/munnar4.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 10,
                    PackageName = "Goa Sundowner",
                    Destination = "Goa",
                    DestinationId = 4,
                    Location = "Calangute",
                    Price = 12500.00m,
                    Duration = "3 Days",
                    ImageUrl = "/lib/TrendingImage/Goa.jpg",
                    ThumbnailUrl1 = " /lib/Image/goa1.jpg",
                    ThumbnailUrl2 = "/lib/Image/goa2.jpg",
                    ThumbnailUrl3 = "/lib/Image/goa3.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 11,
                    PackageName = "Ooty Gardens",
                    Destination = "Tamil Nadu",
                    DestinationId = 2,
                    Location = "Ooty",
                    Price = 52000.00m,
                    Duration = "5 Days",
                    ImageUrl = "/lib/TrendingImage/Ooty.jpg",
                    ThumbnailUrl1 = "/lib/Image/ooty.jpg",
                    ThumbnailUrl2 = "/lib/Image/ooty2.jpg",
                    ThumbnailUrl3 = "/lib/Image/ooty3.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                },
                new TravelPackage
                {
                    PackageId = 12,
                    PackageName = "Sacred Vrindavan",
                    Destination = "Uttar Pradesh",
                    DestinationId = 9,
                    Location = "Vrindavan",
                    Price = 67000.00m,
                    Duration = "3 Days",
                    ImageUrl = "/lib/TrendingImage/Vrindavan.jpg",
                    ThumbnailUrl1 = "/lib/Image/vrindavan5.jpg",
                    ThumbnailUrl2 = "/lib/Image/vrindavan3.jpg",
                    ThumbnailUrl3 = "/lib/Image/vrindavan2.jpg",
                    IsTrending = true,
                    ApprovalStatus = ApprovalStatus.Approved,
                    AvailabilityStatus = PackageStatus.AVAILABLE
                }
            );

            // 3. Seed Gallery Images
            modelBuilder.Entity<GalleryImage>().HasData(
                new GalleryImage { Id = 1, DestinationId = 1, ImageUrl = "/lib/Image/meg1.jpg", Caption = "Krang Suri Falls" },
                new GalleryImage { Id = 2, DestinationId = 1, ImageUrl = "/lib/Image/meg2.jpg", Caption = "Root Bridges" },
                new GalleryImage { Id = 3, DestinationId = 1, ImageUrl = "/lib/Image/meg3.jpg", Caption = "Dawki River" },
                new GalleryImage { Id = 4, DestinationId = 2, ImageUrl = "/lib/Image/t2.jpg", Caption = "Meenakshi Temple" },
                new GalleryImage { Id = 5, DestinationId = 3, ImageUrl = "/lib/Image/k1.jpg", Caption = "Munnar Hills" },
                new GalleryImage { Id = 6, DestinationId = 4, ImageUrl = "/lib/Image/goa3.jpg", Caption = "Baga Beach" }
            );

            // 4. Seed User
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "Admin", Email = "admin@travel.com", Password = "HashedPassword", Role = UserRole.CUSTOMER }
            );
        }
    }
}