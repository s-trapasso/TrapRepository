using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneVeicoli.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddManutenzioniTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Veicoli",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Manutenzioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIntervento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VeicoloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manutenzioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manutenzioni_Veicoli_VeicoloId",
                        column: x => x.VeicoloId,
                        principalTable: "Veicoli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proprietari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietari", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veicoli_ProprietarioId",
                table: "Veicoli",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Manutenzioni_VeicoloId",
                table: "Manutenzioni",
                column: "VeicoloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veicoli_Proprietari_ProprietarioId",
                table: "Veicoli",
                column: "ProprietarioId",
                principalTable: "Proprietari",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veicoli_Proprietari_ProprietarioId",
                table: "Veicoli");

            migrationBuilder.DropTable(
                name: "Manutenzioni");

            migrationBuilder.DropTable(
                name: "Proprietari");

            migrationBuilder.DropIndex(
                name: "IX_Veicoli_ProprietarioId",
                table: "Veicoli");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Veicoli");
        }
    }
}
