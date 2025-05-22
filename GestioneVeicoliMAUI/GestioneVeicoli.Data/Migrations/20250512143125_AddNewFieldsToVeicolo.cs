using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneVeicoli.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToVeicolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alimentazione",
                table: "Veicoli",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Km",
                table: "Veicoli",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alimentazione",
                table: "Veicoli");

            migrationBuilder.DropColumn(
                name: "Km",
                table: "Veicoli");
        }
    }
}
