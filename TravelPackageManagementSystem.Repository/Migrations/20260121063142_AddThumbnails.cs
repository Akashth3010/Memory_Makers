using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl1",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl2",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl3",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 1,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/meg4.jpg", "/lib/Image/meghbg.jpg", "/lib/Image/meg3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 2,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/t4.jpg", "/lib/Image/t25.jpg", "/lib/Image/t3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 3,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/k32.jpg", "/lib/Image/k35.jpg", "/lib/Image/k3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 4,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/TrendingImage/mumbai1.jpg", "/lib/TrendingImage/mumbai2.jpg", "/lib/TrendingImage/mumbai3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 5,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/TrendingImage/jaipur1.jpg", "/lib/TrendingImage/jaipur2.jpg", "/lib/TrendingImage/jaipur3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 6,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/TrendingImage/gangtok1.jpg", "/lib/TrendingImage/gangtok2.jpg", "/lib/TrendingImage/gangtok3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 7,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/darjiling1.jpg", "/lib/Image/darjiling2.jpg", "/lib/Image/darjiling3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 8,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/banaras1.jpg", "/lib/Image/banarsi2.jpg", "/lib/Image/banaras3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 9,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { " /lib/Image/munnar1.jpg", "/lib/Image/munnar2.jpg", "/lib/Image/munnar4.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 10,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { " /lib/Image/goa1.jpg", "/lib/Image/goa2.jpg", "/lib/Image/goa3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 11,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/ooty.jpg", "/lib/Image/ooty2.jpg", "/lib/Image/ooty3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 12,
                columns: new[] { "ImageUrl", "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/TrendingImage/Vrindavan.jpg", "/lib/Image/vrindavan1.jpg", "/lib/Image/vrindavan2.jpg", "/lib/Image/vrindavan3.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl1",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl2",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl3",
                table: "TravelPackages");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 12,
                column: "ImageUrl",
                value: "/lib/TrendingImage/Banaras.jpg");
        }
    }
}
