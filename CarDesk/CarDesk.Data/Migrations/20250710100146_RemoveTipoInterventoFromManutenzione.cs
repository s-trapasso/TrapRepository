using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTipoInterventoFromManutenzione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoIntervento",
                table: "Manutenzioni");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoIntervento",
                table: "Manutenzioni",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
