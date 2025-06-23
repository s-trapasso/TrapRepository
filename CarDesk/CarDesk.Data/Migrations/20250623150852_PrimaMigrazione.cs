using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimaMigrazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proprietari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veicoli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Targa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Modello = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Anno = table.Column<int>(type: "int", nullable: false),
                    Alimentazione = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Km = table.Column<int>(type: "int", nullable: true),
                    ProprietarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veicoli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veicoli_Proprietari_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Manutenzioni_VeicoloId",
                table: "Manutenzioni",
                column: "VeicoloId");

            migrationBuilder.CreateIndex(
                name: "IX_Veicoli_ProprietarioId",
                table: "Veicoli",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Veicoli_Targa",
                table: "Veicoli",
                column: "Targa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manutenzioni");

            migrationBuilder.DropTable(
                name: "Veicoli");

            migrationBuilder.DropTable(
                name: "Proprietari");
        }
    }
}
