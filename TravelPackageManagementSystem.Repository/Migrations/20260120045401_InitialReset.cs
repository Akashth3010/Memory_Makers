using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPackageManagementSystem.Repository.Migrations
{
    public partial class InitialReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // We removed all "CreateTable" blocks because those tables already exist in your DB.
            // We only keep the "AddColumn" commands to add the new tracking fields.

            migrationBuilder.AddColumn<bool>(
                name: "IsLoggedIn",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // This allows you to undo the changes if needed
            migrationBuilder.DropColumn(
                name: "IsLoggedIn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "Users");
        }
    }
}