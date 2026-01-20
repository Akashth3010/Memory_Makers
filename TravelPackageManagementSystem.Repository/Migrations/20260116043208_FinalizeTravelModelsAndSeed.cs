using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FinalizeTravelModelsAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TravelPackages_PackageId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PackageId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Bookings");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TravelPackages",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PackageName",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsTrending",
                table: "TravelPackages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TravelPackagePackageId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 1,
                columns: new[] { "Description", "Destination", "Duration", "ImageUrl", "IsTrending", "Location", "PackageName", "Price" },
                values: new object[] { null, "Meghalaya", "3 Days", "/lib/Image/meghalaya.jpg", false, "Shillong Peak", "Quick Escape", 9999.00m });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 2,
                columns: new[] { "Description", "Destination", "Duration", "ImageUrl", "IsTrending", "Location", "PackageName", "Price" },
                values: new object[] { null, "Meghalaya", "5 Days", "/lib/Image/Waterfall.jpg", true, "Root Bridges", "The Classic", 17999.00m });

            migrationBuilder.InsertData(
                table: "TravelPackages",
                columns: new[] { "PackageId", "Description", "Destination", "Duration", "ImageUrl", "IsTrending", "Location", "PackageName", "Price", "Status" },
                values: new object[] { 3, null, "Meghalaya", "7 Days", "/lib/Image/meghbg.jpg", false, "Hidden Caves", "Deep Explorer", 25999.00m, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TravelPackagePackageId",
                table: "Bookings",
                column: "TravelPackagePackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TravelPackages_TravelPackagePackageId",
                table: "Bookings",
                column: "TravelPackagePackageId",
                principalTable: "TravelPackages",
                principalColumn: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TravelPackages_TravelPackagePackageId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TravelPackagePackageId",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "IsTrending",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "TravelPackagePackageId",
                table: "Bookings");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TravelPackages",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PackageName",
                table: "TravelPackages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "TravelPackages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "TravelPackages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 1,
                columns: new[] { "Destination", "Duration", "PackageName", "Price" },
                values: new object[] { "France", 5, "Paris Getaway", 1200.00m });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 2,
                columns: new[] { "Destination", "Duration", "PackageName", "Price" },
                values: new object[] { "Japan", 7, "Tokyo Adventure", 1500.00m });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PackageId",
                table: "Bookings",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TravelPackages_PackageId",
                table: "Bookings",
                column: "PackageId",
                principalTable: "TravelPackages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
