using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FinalizeDestinationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "TravelPackages");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "TravelPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "DestinationId", "HolidayCount", "HotelCount", "ImageUrl", "StateName" },
                values: new object[,]
                {
                    { 1, 29, 18, "/lib/Image/meghalaya.jpg", "Meghalaya" },
                    { 2, 48, 30, "/lib/Image/Tamilnadu.jpg", "Tamil Nadu" },
                    { 3, 36, 22, "/lib/Image/Kerala.jpg", "Kerala" },
                    { 4, 40, 25, "/lib/Image/goa.jpg", "Goa" }
                });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 1,
                column: "DestinationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 2,
                column: "DestinationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 3,
                column: "DestinationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 4,
                column: "DestinationId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 5,
                column: "DestinationId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 6,
                column: "DestinationId",
                value: 4);

            migrationBuilder.CreateIndex(
                name: "IX_TravelPackages_DestinationId",
                table: "TravelPackages",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackages_Destinations_DestinationId",
                table: "TravelPackages",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackages_Destinations_DestinationId",
                table: "TravelPackages");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_TravelPackages_DestinationId",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "TravelPackages");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 1,
                column: "Destination",
                value: "Meghalaya");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 2,
                column: "Destination",
                value: "Meghalaya");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 3,
                column: "Destination",
                value: "Meghalaya");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 4,
                column: "Destination",
                value: "Tamil Nadu");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 5,
                column: "Destination",
                value: "Kerala");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 6,
                column: "Destination",
                value: "Goa");
        }
    }
}
