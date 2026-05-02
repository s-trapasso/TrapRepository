using System.ComponentModel.DataAnnotations;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Shared.Entities;

/// <summary>
/// Rappresenta un veicolo (auto, moto, furgone, ecc.)
/// </summary>
public class Vehicle : BaseEntity
{
    [Required]
    [StringLength(10)]
    public string Targa { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Marca { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Modello { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Versione { get; set; }         // es. "1.6 TDI Sport"

    [Range(1900, 2100)]
    public int Anno { get; set; }

    public VehicleType Tipo { get; set; } = VehicleType.Automobile;

    public FuelType Carburante { get; set; } = FuelType.Benzina;

    [StringLength(30)]
    public string? Colore { get; set; }

    [StringLength(17)]
    public string? Vin { get; set; }              // Vehicle Identification Number (telaio)

    public int KmAttuali { get; set; }

    public DateOnly? DataImmatricolazione { get; set; }

    public DateOnly? DataAcquisto { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? PrezzoAcquisto { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; }

    public bool Attivo { get; set; } = true;      // false = venduto/rottamato

    // ── Relazioni ────────────────────────────────────────────────────────────
    public ICollection<Maintenance> Manutenzioni { get; set; } = new List<Maintenance>();
    public ICollection<VehicleOwnership> Proprietari { get; set; } = new List<VehicleOwnership>();
    public ICollection<Deadline> Scadenze { get; set; } = new List<Deadline>();
    public ICollection<VehicleDocument> Documenti { get; set; } = new List<VehicleDocument>();
}