using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntiCampiPatente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoriaPatente",
                table: "Proprietari",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRilascioPatente",
                table: "Proprietari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataScadenzaPatente",
                table: "Proprietari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NumeroPatente",
                table: "Proprietari",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaPatente",
                table: "Proprietari");

            migrationBuilder.DropColumn(
                name: "DataRilascioPatente",
                table: "Proprietari");

            migrationBuilder.DropColumn(
                name: "DataScadenzaPatente",
                table: "Proprietari");

            migrationBuilder.DropColumn(
                name: "NumeroPatente",
                table: "Proprietari");
        }
    }
}
