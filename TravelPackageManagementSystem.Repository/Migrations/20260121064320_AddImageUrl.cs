using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 4,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/mumbai1.jpg", "/lib/Image/mumbai2.jpg", "/lib/Image/mumbai3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 5,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/jaipur1.jpg", "/lib/Image/jaipur2.jpg", "/lib/Image/jaipur3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 6,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/gangtok1.jpg", "/lib/Image/gangtok2.jpg", "/lib/Image/gangtok3.jpg" });

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 8,
                column: "ThumbnailUrl2",
                value: "/lib/Image/banaras2.jpg");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 12,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/vrindavan5.jpg", "/lib/Image/vrindavan3.jpg", "/lib/Image/vrindavan2.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 8,
                column: "ThumbnailUrl2",
                value: "/lib/Image/banarsi2.jpg");

            migrationBuilder.UpdateData(
                table: "TravelPackages",
                keyColumn: "PackageId",
                keyValue: 12,
                columns: new[] { "ThumbnailUrl1", "ThumbnailUrl2", "ThumbnailUrl3" },
                values: new object[] { "/lib/Image/vrindavan1.jpg", "/lib/Image/vrindavan2.jpg", "/lib/Image/vrindavan3.jpg" });
        }
    }
}
