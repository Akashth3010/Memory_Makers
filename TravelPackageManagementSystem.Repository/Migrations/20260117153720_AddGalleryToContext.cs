using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelPackageManagementSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddGalleryToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GalleryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryImages_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GalleryImages",
                columns: new[] { "Id", "Caption", "DestinationId", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Krang Suri Falls", 1, "/lib/Image/meg1.jpg" },
                    { 2, "Root Bridges", 1, "/lib/Image/meg2.jpg" },
                    { 3, "Dawki River", 1, "/lib/Image/meg3.jpg" },
                    { 4, "Meenakshi Temple", 2, "/lib/Image/Tamil1.jpg" },
                    { 5, "Munnar Hills", 3, "/lib/Image/Kerala1.jpg" },
                    { 6, "Baga Beach", 4, "/lib/Image/Goa1.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImages_DestinationId",
                table: "GalleryImages",
                column: "DestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryImages");
        }
    }
}
