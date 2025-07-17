using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaUnicitaCodiceFiscale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodiceFiscale",
                table: "Proprietari",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascita",
                table: "Proprietari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LuogoNascita",
                table: "Proprietari",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sesso",
                table: "Proprietari",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Proprietari_CodiceFiscale",
                table: "Proprietari",
                column: "CodiceFiscale",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Proprietari_CodiceFiscale",
                table: "Proprietari");

            migrationBuilder.DropColumn(
                name: "CodiceFiscale",
                table: "Proprietari");

            migrationBuilder.DropColumn(
                name: "DataNascita",
                table: "Proprietari");

            migrationBuilder.DropColumn(
                name: "LuogoNascita",
                table: "Proprietari");

            migrationBuilder.DropColumn(
                name: "Sesso",
                table: "Proprietari");
        }
    }
}
