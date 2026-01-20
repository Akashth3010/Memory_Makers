using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TravelPackages",
                columns: new[] { "PackageId", "Description", "Destination", "Duration", "ImageUrl", "IsTrending", "Location", "PackageName", "Price", "Status" },
                values: new object[,]
                {
                    { 4, null, "Tamil Nadu", "48 Days", "/lib/Image/Tamilnadu.jpg", false, null, "Temple Trail", 18500m, 0 },
                    { 5, null, "Kerala", "36 Days", "/lib/Image/Kerala.jpg", false, null, "Backwater Bliss", 22000m, 0 },
                    { 6, null, "Goa", "40 Days", "/lib/Image/goa.jpg", false, null, "Sun & Sand", 15000m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 6);
        }
    }
}
