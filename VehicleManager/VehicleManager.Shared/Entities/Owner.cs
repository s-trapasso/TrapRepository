using System.ComponentModel.DataAnnotations;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Shared.Entities;

/// <summary>
/// Anagrafica proprietario/utilizzatore del veicolo.
/// </summary>
public class Owner : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Cognome { get; set; } = string.Empty;

    [StringLength(16)]
    public string? CodiceFiscale { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(200)]
    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(200)]
    public string? Indirizzo { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }

    // ── Proprietà calcolata ───────────────────────────────────────────────────
    public string NomeCompleto => $"{Nome} {Cognome}";

    // ── Relazioni ────────────────────────────────────────────────────────────
    public ICollection<VehicleOwnership> Veicoli { get; set; } = new List<VehicleOwnership>();

    [StringLength(20)]
    public string? PatenteNumero { get; set; }

    [StringLength(10)]
    public string? PatenteCategoria { get; set; }    // es. "A", "B", "A/B"

    public DateOnly? PatenteScadenza { get; set; }

    public DateOnly? PatenteMedicaScadenza { get; set; }

    // Proprietà calcolate (non salvate nel DB)
    public DeadlineStatus StatoPatente => CalcolaStato(PatenteScadenza);
    public DeadlineStatus StatoVisitaMedica => CalcolaStato(PatenteMedicaScadenza);

    private static DeadlineStatus CalcolaStato(DateOnly? scadenza)
    {
        if (scadenza == null) return DeadlineStatus.Valida;
        var giorni = (int)(scadenza.Value.ToDateTime(TimeOnly.MinValue) - DateTime.Today).TotalDays;
        return giorni switch
        {
            < 0 => DeadlineStatus.Scaduta,
            <= 30 => DeadlineStatus.InScadenza,
            _ => DeadlineStatus.Valida
        };
    }
}