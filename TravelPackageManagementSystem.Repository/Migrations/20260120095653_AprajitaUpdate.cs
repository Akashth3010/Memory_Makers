using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AprajitaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelCount = table.Column<int>(type: "int", nullable: false),
                    HolidayCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DestinationId);
                });

            migrationBuilder.CreateTable(
                name: "HostContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostAgencyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CityCountry = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostContactDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "GalleryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryImages_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelPackages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PackageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTrending = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    AvailabilityStatus = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPackages", x => x.PackageId);
                    table.ForeignKey(
                        name: "FK_TravelPackages_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelPackages_HostContactDetails_HostId",
                        column: x => x.HostId,
                        principalTable: "HostContactDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guests = table.Column<int>(type: "int", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_TravelPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "TravelPackages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itineraries",
                columns: table => new
                {
                    ItineraryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    ActivityTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActivityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inclusions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exclusions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itineraries", x => x.ItineraryId);
                    table.ForeignKey(
                        name: "FK_Itineraries_TravelPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "TravelPackages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "DestinationId", "HolidayCount", "HotelCount", "ImageUrl", "StateName" },
                values: new object[,]
                {
                    { 1, 29, 18, "/lib/Image/meghalaya.jpg", "Meghalaya" },
                    { 2, 48, 30, "/lib/Image/Tamilnadu.jpg", "Tamil Nadu" },
                    { 3, 36, 22, "/lib/Image/Kerala.jpg", "Kerala" },
                    { 4, 40, 25, "/lib/Image/goa.jpg", "Goa" },
                    { 5, 60, 45, "/lib/TrendingImage/Mumbai.jpg", "Maharashtra" },
                    { 6, 75, 50, "/lib/TrendingImage/Hawa Mahal Jaipur.jpg", "Rajasthan" },
                    { 7, 30, 20, "/lib/TrendingImage/Darjiling.jpg", "West Bengal" },
                    { 8, 25, 15, "/lib/TrendingImage/Gangtok.jpg", "Sikkim" },
                    { 9, 55, 40, "/lib/TrendingImage/Banaras.jpg", "Uttar Pradesh" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "Role", "Username" },
                values: new object[] { 1, "admin@travel.com", "HashedPassword", 2, "Admin" });

            migrationBuilder.InsertData(
                table: "GalleryImages",
                columns: new[] { "Id", "Caption", "DestinationId", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Krang Suri Falls", 1, "/lib/Image/meg1.jpg" },
                    { 2, "Root Bridges", 1, "/lib/Image/meg2.jpg" },
                    { 3, "Dawki River", 1, "/lib/Image/meg3.jpg" },
                    { 4, "Meenakshi Temple", 2, "/lib/Image/Tamil1.jpg" },
                    { 5, "Munnar Hills", 3, "/lib/Image/Kerala1.jpg" },
                    { 6, "Baga Beach", 4, "/lib/Image/Goa1.jpg" }
                });

            migrationBuilder.InsertData(
                table: "TravelPackages",
                columns: new[] { "PackageId", "ApprovalStatus", "AvailabilityStatus", "Description", "Destination", "DestinationId", "Duration", "HostId", "ImageUrl", "IsTrending", "Location", "PackageName", "PackageType", "Price", "status" },
                values: new object[,]
                {
                    { 1, 1, 0, null, "Meghalaya", 1, "3 Days", null, "/lib/Image/meghalaya.jpg", false, "Shillong Peak", "Quick Escape", "", 9999.00m, 0 },
                    { 2, 1, 0, null, "Tamil Nadu", 2, "8 Days", null, "/lib/Image/Tamilnadu.jpg", false, "Madurai", "Temple Trail", "", 18500.00m, 0 },
                    { 3, 1, 0, null, "Kerala", 3, "6 Days", null, "/lib/Image/Kerala.jpg", false, "Alleppey", "Backwater Bliss", "", 22000.00m, 0 },
                    { 4, 1, 0, "A bustling metropolis blending colonial heritage and Bollywood glamour.", "Maharashtra", 5, "5 Days", null, "/lib/TrendingImage/Mumbai.jpg", true, "Gateway of India", "Mumbai Heritage", "", 35000.00m, 0 },
                    { 5, 1, 0, null, "Rajasthan", 6, "6 Days", null, "/lib/TrendingImage/Hawa Mahal Jaipur.jpg", true, "Jaipur", "Pink City Tour", "", 65000.00m, 0 },
                    { 6, 1, 0, null, "Sikkim", 8, "8 Days", null, "/lib/TrendingImage/Gangtok.jpg", true, "Gangtok", "Sikkim Adventure", "", 70000.00m, 0 },
                    { 7, 1, 0, null, "West Bengal", 7, "4 Days", null, "/lib/TrendingImage/Darjiling.jpg", true, "Darjeeling", "Darjeeling Tea", "", 5500.00m, 0 },
                    { 8, 1, 0, null, "Uttar Pradesh", 9, "3 Days", null, "/lib/TrendingImage/Banaras.jpg", true, "Varanasi", "Varanasi Spiritual", "", 83000.00m, 0 },
                    { 9, 1, 0, null, "Kerala", 3, "5 Days", null, "/lib/TrendingImage/Munnar.jpg", true, "Munnar", "Misty Munnar", "", 50000.00m, 0 },
                    { 10, 1, 0, null, "Goa", 4, "3 Days", null, "/lib/TrendingImage/Goa.jpg", true, "Calangute", "Goa Sundowner", "", 12500.00m, 0 },
                    { 11, 1, 0, null, "Tamil Nadu", 2, "5 Days", null, "/lib/TrendingImage/Ooty.jpg", true, "Ooty", "Ooty Gardens", "", 52000.00m, 0 },
                    { 12, 1, 0, null, "Uttar Pradesh", 9, "3 Days", null, "/lib/TrendingImage/Banaras.jpg", true, "Vrindavan", "Sacred Vrindavan", "", 67000.00m, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PackageId",
                table: "Bookings",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImages_DestinationId",
                table: "GalleryImages",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Itineraries_PackageId",
                table: "Itineraries",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPackages_DestinationId",
                table: "TravelPackages",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPackages_HostId",
                table: "TravelPackages",
                column: "HostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryImages");

            migrationBuilder.DropTable(
                name: "Itineraries");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "TravelPackages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "HostContactDetails");
        }
    }
}
