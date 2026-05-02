using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Indirizzo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Targa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Modello = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Versione = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Anno = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Carburante = table.Column<int>(type: "int", nullable: false),
                    Colore = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Vin = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    KmAttuali = table.Column<int>(type: "int", nullable: false),
                    DataImmatricolazione = table.Column<DateOnly>(type: "date", nullable: true),
                    DataAcquisto = table.Column<DateOnly>(type: "date", nullable: true),
                    PrezzoAcquisto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Attivo = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deadlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataScadenza = table.Column<DateOnly>(type: "date", nullable: false),
                    DataRinnovo = table.Column<DateOnly>(type: "date", nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Compagnia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumeroPolizza = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NotificaAttiva = table.Column<bool>(type: "bit", nullable: false),
                    GiorniPreavviso = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deadlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deadlines_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataIntervento = table.Column<DateOnly>(type: "date", nullable: false),
                    KmAlMomento = table.Column<int>(type: "int", nullable: true),
                    KmProssimoIntervento = table.Column<int>(type: "int", nullable: true),
                    DataProssimoIntervento = table.Column<DateOnly>(type: "date", nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Officina = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumeroFattura = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenances_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleOwnerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    TipoProprietà = table.Column<int>(type: "int", nullable: false),
                    DataAcquisto = table.Column<DateOnly>(type: "date", nullable: false),
                    DataCessione = table.Column<DateOnly>(type: "date", nullable: true),
                    PrezzoAcquisto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PrezzoVendita = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    KmAlAcquisto = table.Column<int>(type: "int", nullable: true),
                    KmAllaCessione = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleOwnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleOwnerships_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleOwnerships_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceId = table.Column<int>(type: "int", nullable: true),
                    NomeFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PathFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DimensioneBytes = table.Column<long>(type: "bigint", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsFoto = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleDocuments_Maintenances_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "Maintenances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleDocuments_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deadlines_VehicleId_DataScadenza",
                table: "Deadlines",
                columns: new[] { "VehicleId", "DataScadenza" });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_VehicleId_DataIntervento",
                table: "Maintenances",
                columns: new[] { "VehicleId", "DataIntervento" });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDocuments_MaintenanceId",
                table: "VehicleDocuments",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDocuments_VehicleId_IsFoto",
                table: "VehicleDocuments",
                columns: new[] { "VehicleId", "IsFoto" });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOwnerships_OwnerId",
                table: "VehicleOwnerships",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOwnerships_VehicleId_DataCessione",
                table: "VehicleOwnerships",
                columns: new[] { "VehicleId", "DataCessione" });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Targa",
                table: "Vehicles",
                column: "Targa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deadlines");

            migrationBuilder.DropTable(
                name: "VehicleDocuments");

            migrationBuilder.DropTable(
                name: "VehicleOwnerships");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
