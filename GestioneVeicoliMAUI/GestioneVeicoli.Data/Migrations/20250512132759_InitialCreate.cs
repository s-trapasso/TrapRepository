using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneVeicoli.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veicoli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Targa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Modello = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Anno = table.Column<int>(type: "int", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veicoli", x => x.Id);
                });

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
                name: "Veicoli");
        }
    }
}
