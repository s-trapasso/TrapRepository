using System.ComponentModel.DataAnnotations;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Shared.Entities;

/// <summary>
/// Storico di un intervento di manutenzione su un veicolo.
/// </summary>
public class Maintenance : BaseEntity
{
    // ── Chiave esterna ────────────────────────────────────────────────────────
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    // ── Dati intervento ───────────────────────────────────────────────────────
    public MaintenanceType Tipo { get; set; }

    [Required]
    [StringLength(200)]
    public string Descrizione { get; set; } = string.Empty;

    public DateOnly DataIntervento { get; set; }

    public int? KmAlMomento { get; set; }

    public int? KmProssimoIntervento { get; set; }   // es. prossimo tagliando a 50.000 km

    public DateOnly? DataProssimoIntervento { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Costo { get; set; }

    [StringLength(200)]
    public string? Officina { get; set; }

    [StringLength(50)]
    public string? NumeroFattura { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; }

    // ── Relazioni ────────────────────────────────────────────────────────────
    public ICollection<VehicleDocument> Documenti { get; set; } = new List<VehicleDocument>();
}