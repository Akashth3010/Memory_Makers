using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class TrendingPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Itineraries",
                keyColumn: "ItineraryId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 4,
                columns: new[] { "IsTrending", "Location" },
                values: new object[] { true, "Madurai" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 5,
                columns: new[] { "IsTrending", "Location" },
                values: new object[] { true, "Alleppey" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 6,
                columns: new[] { "IsTrending", "Location" },
                values: new object[] { true, "Calangute" });

            migrationBuilder.InsertData(
                table: "TravelPackages",
                columns: new[] { "PackageId", "Description", "DestinationId", "Duration", "ImageUrl", "IsTrending", "Location", "PackageName", "Price", "Status" },
                values: new object[,]
                {
                    { 7, "A bustling metropolis blending colonial heritage and Bollywood glamour.", 2, "5 Days", "/lib/TrendingImage/Mumbai.jpg", true, "Gateway of India", "Mumbai Heritage", 35000.00m, 0 },
                    { 8, null, 2, "6 Days", "/lib/TrendingImage/Hawa Mahal Jaipur.jpg", true, "Jaipur", "Pink City Tour", 65000.00m, 0 },
                    { 9, null, 1, "8 Days", "/lib/TrendingImage/Gangtok.jpg", true, "Gangtok", "Sikkim Adventure", 70000.00m, 0 },
                    { 10, null, 1, "4 Days", "/lib/TrendingImage/Darjiling.jpg", true, "Darjeeling", "Darjeeling Tea", 5500.00m, 0 },
                    { 11, null, 2, "5 Days", "/lib/TrendingImage/Ooty.jpg", true, "Ooty", "Ooty Gardens", 52000.00m, 0 },
                    { 12, null, 2, "3 Days", "/lib/TrendingImage/Banaras.jpg", true, "Varanasi", "Varanasi Spiritual", 83000.00m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 12);

            migrationBuilder.InsertData(
                table: "Itineraries",
                columns: new[] { "ItineraryId", "ActivityDescription", "ActivityTitle", "DayNumber", "Exclusions", "Inclusions", "PackageId" },
                values: new object[] { 2, "Breathtaking views...", "Laitlum Canyons", 2, "Airfare;Lunch;Personal Expenses", "Resort Stay;Daily Breakfast;Private SUV", 3 });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 4,
                columns: new[] { "IsTrending", "Location" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 5,
                columns: new[] { "IsTrending", "Location" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 6,
                columns: new[] { "IsTrending", "Location" },
                values: new object[] { false, null });
        }
    }
}
