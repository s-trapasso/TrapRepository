using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggiungiVoceIntervento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VoceIntervento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ManutenzioneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoceIntervento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoceIntervento_Manutenzioni_ManutenzioneId",
                        column: x => x.ManutenzioneId,
                        principalTable: "Manutenzioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoceIntervento_ManutenzioneId",
                table: "VoceIntervento",
                column: "ManutenzioneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoceIntervento");
        }
    }
}
