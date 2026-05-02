using System.ComponentModel.DataAnnotations;

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
}