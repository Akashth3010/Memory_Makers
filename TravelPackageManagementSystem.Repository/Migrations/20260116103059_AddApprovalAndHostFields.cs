using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovalAndHostFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "TravelPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostId",
                table: "TravelPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PackageType",
                table: "TravelPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPackages_HostId",
                table: "TravelPackages",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelPackages_HostContactDetails_HostId",
                table: "TravelPackages",
                column: "HostId",
                principalTable: "HostContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelPackages_HostContactDetails_HostId",
                table: "TravelPackages");

            migrationBuilder.DropIndex(
                name: "IX_TravelPackages_HostId",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "TravelPackages");

            migrationBuilder.DropColumn(
                name: "PackageType",
                table: "TravelPackages");
        }
    }
}
