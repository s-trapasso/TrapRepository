using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTireFieldsToVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTireType",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTireChangeDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTireType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "LastTireChangeDate",
                table: "Vehicles");
        }
    }
}
