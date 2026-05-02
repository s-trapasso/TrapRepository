using System.ComponentModel.DataAnnotations;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Shared.Entities;

/// <summary>
/// Scadenza associata a un veicolo (bollo, assicurazione, revisione, ecc.)
/// </summary>
public class Deadline : BaseEntity
{
    // ── Chiave esterna ────────────────────────────────────────────────────────
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    // ── Dati scadenza ─────────────────────────────────────────────────────────
    public DeadlineType Tipo { get; set; }

    [StringLength(200)]
    public string? Descrizione { get; set; }        // dettaglio libero, es. "Zurich RC + Furto"

    public DateOnly DataScadenza { get; set; }

    public DateOnly? DataRinnovo { get; set; }       // data in cui è stata rinnovata

    [Range(0, double.MaxValue)]
    public decimal? Costo { get; set; }

    [StringLength(200)]
    public string? Compagnia { get; set; }           // es. nome assicurazione, ACI, ecc.

    [StringLength(100)]
    public string? NumeroPolizza { get; set; }

    public bool NotificaAttiva { get; set; } = true;

    public int GiorniPreavviso { get; set; } = 30;  // quanti giorni prima avvisare

    [StringLength(500)]
    public string? Note { get; set; }

    // ── Proprietà calcolate ───────────────────────────────────────────────────
    public int GiorniAllaScadenza =>
        (int)(DataScadenza.ToDateTime(TimeOnly.MinValue) - DateTime.Today).TotalDays;

    public DeadlineStatus Stato => GiorniAllaScadenza switch
    {
        < 0 => DeadlineStatus.Scaduta,
        <= 30 => DeadlineStatus.InScadenza,
        _ => DeadlineStatus.Valida
    };
}