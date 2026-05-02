using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Shared.Entities;

/// <summary>
/// Documento allegato a un veicolo o a una manutenzione.
/// Il file fisico viene salvato su disco; qui si conservano i metadati.
/// </summary>
public class VehicleDocument : BaseEntity
{
    // ── Chiavi esterne ────────────────────────────────────────────────────────
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    public int? MaintenanceId { get; set; }          // null = documento del veicolo, non di una manutenzione
    public Maintenance? Maintenance { get; set; }

    // ── Dati documento ────────────────────────────────────────────────────────
    [Required]
    [StringLength(200)]
    public string NomeFile { get; set; } = string.Empty;     // nome originale del file

    [Required]
    [StringLength(500)]
    public string PathFile { get; set; } = string.Empty;     // percorso su disco

    [StringLength(100)]
    public string? MimeType { get; set; }                    // es. "application/pdf", "image/jpeg"

    public long DimensioneBytes { get; set; }

    [StringLength(200)]
    public string? Descrizione { get; set; }                 // es. "Fattura tagliando", "Foto frontale"

    public bool IsFoto { get; set; } = false;                // per distinguere rapidamente foto da documenti
}