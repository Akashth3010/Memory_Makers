using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId1",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId1",
                table: "Payments",
                column: "BookingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId1",
                table: "Payments",
                column: "BookingId1",
                principalTable: "Bookings",
                principalColumn: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BookingId1",
                table: "Payments");
        }
    }
}
