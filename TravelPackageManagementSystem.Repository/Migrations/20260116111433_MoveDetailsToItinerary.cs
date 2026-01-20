using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class MoveDetailsToItinerary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exclusions",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "Inclusions",
                table: "TravelPackages");

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "ItineraryId",
                keyValue: 1,
                columns: new[] { "Exclusions", "Inclusions" },
                values: new object[] { "Airfare;Lunch;Personal Expenses", "Resort Stay;Daily Breakfast;Private SUV" });

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "ItineraryId",
                keyValue: 2,
                columns: new[] { "Exclusions", "Inclusions" },
                values: new object[] { "Airfare;Lunch;Personal Expenses", "Resort Stay;Daily Breakfast;Private SUV" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Exclusions",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Inclusions",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "ItineraryId",
                keyValue: 1,
                columns: new[] { "Exclusions", "Inclusions" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "ItineraryId",
                keyValue: 2,
                columns: new[] { "Exclusions", "Inclusions" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 1,
                columns: new[] { "Exclusions", "Inclusions" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 2,
                columns: new[] { "Exclusions", "Inclusions" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 3,
                columns: new[] { "Exclusions", "Inclusions" },
                values: new object[] { "", "" });
        }
    }
}
