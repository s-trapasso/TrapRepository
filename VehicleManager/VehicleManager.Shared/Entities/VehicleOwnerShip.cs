using System.ComponentModel.DataAnnotations;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Shared.Entities;

/// <summary>
/// Storico delle proprietà di un veicolo.
/// Tiene traccia di chi ha posseduto il veicolo, quando e a quale prezzo.
/// </summary>
public class VehicleOwnership : BaseEntity
{
    // ── Chiavi esterne ────────────────────────────────────────────────────────
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    public int OwnerId { get; set; }
    public Owner Owner { get; set; } = null!;

    // ── Dati proprietà ────────────────────────────────────────────────────────
    public OwnershipType TipoProprietà { get; set; } = OwnershipType.Proprietario;

    public DateOnly DataAcquisto { get; set; }

    public DateOnly? DataCessione { get; set; }    // null = proprietario attuale

    [Range(0, double.MaxValue)]
    public decimal? PrezzoAcquisto { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? PrezzoVendita { get; set; }

    public int? KmAlAcquisto { get; set; }

    public int? KmAllaCessione { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }

    // ── Proprietà calcolata ───────────────────────────────────────────────────
    public bool IsAttuale => DataCessione == null;
}