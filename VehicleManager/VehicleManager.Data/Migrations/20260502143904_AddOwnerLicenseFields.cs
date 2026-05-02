using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerLicenseFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatenteCategoria",
                table: "Owners",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PatenteMedicaScadenza",
                table: "Owners",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatenteNumero",
                table: "Owners",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PatenteScadenza",
                table: "Owners",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatenteCategoria",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "PatenteMedicaScadenza",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "PatenteNumero",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "PatenteScadenza",
                table: "Owners");
        }
    }
}
