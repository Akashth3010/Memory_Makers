using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class HostDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 2);

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

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Guests",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TravelDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HostContactDetails");

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
                name: "ContactPhone",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Guests",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TravelDate",
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

            migrationBuilder.InsertData(
                table: "TravelPackages",
                columns: new[] { "PackageId", "Destination", "Duration", "PackageName", "Price", "Status" },
                values: new object[,]
                {
                    { 1, "France", 5, "Paris Getaway", 1200.00m, 0 },
                    { 2, "Japan", 7, "Tokyo Adventure", 1500.00m, 0 }
                });
        }
    }
}
