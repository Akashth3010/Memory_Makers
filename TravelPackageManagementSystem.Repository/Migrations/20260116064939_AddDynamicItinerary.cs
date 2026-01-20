using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddDynamicItinerary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivityTitle",
                table: "Itineraries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Itineraries",
                columns: new[] { "ItineraryId", "ActivityDescription", "ActivityTitle", "DayNumber", "PackageId" },
                values: new object[,]
                {
                    { 1, "Pick up from Guwahati...", "Arrival in Shillong", 1, 3 },
                    { 2, "Breathtaking views...", "Laitlum Canyons", 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Itineraries",
                keyColumn: "ItineraryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Itineraries",
                keyColumn: "ItineraryId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ActivityTitle",
                table: "Itineraries");
        }
    }
}
