using System.ComponentModel.DataAnnotations;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Shared.DTOs;

// ── Risposta API (lettura) ────────────────────────────────────────────────────

public class VehicleDto
{
    public int Id { get; set; }
    public string Targa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modello { get; set; } = string.Empty;
    public string? Versione { get; set; }
    public int Anno { get; set; }
    public VehicleType Tipo { get; set; }
    public string TipoLabel => Tipo.ToString();
    public FuelType Carburante { get; set; }
    public string CarburanteLabel => Carburante.ToString();
    public string? Colore { get; set; }
    public string? Vin { get; set; }
    public int KmAttuali { get; set; }
    public DateOnly? DataImmatricolazione { get; set; }
    public DateOnly? DataAcquisto { get; set; }
    public decimal? PrezzoAcquisto { get; set; }
    public string? Note { get; set; }
    public bool Attivo { get; set; }
    public DateTime CreatedAt { get; set; }

    // Dati sintetici per la lista/dashboard
    public string? ProprietarioAttuale { get; set; }
    public int NumeroManutenzioni { get; set; }
    public int ScadenzeInScadenza { get; set; }
    public int ScadenzeScadute { get; set; }
    public List<MaintenanceDto> Manutenzioni { get; set; } = [];
    public List<DeadlineDto> Scadenze { get; set; } = [];
    public List<VehicleOwnershipDto> Proprietari { get; set; } = [];
}

// ── Riepilogo per lista/dashboard (più leggero) ───────────────────────────────

public class VehicleSummaryDto
{
    public int Id { get; set; }
    public string Targa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modello { get; set; } = string.Empty;
    public int Anno { get; set; }
    public VehicleType Tipo { get; set; }
    public string TipoLabel => Tipo.ToString();
    public int KmAttuali { get; set; }
    public bool Attivo { get; set; }
    public string? ProprietarioAttuale { get; set; }
    public int ScadenzeScadute { get; set; }
    public int ScadenzeInScadenza { get; set; }
}

// ── Creazione ─────────────────────────────────────────────────────────────────

public class CreateVehicleDto
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
    public string? Versione { get; set; }

    [Range(1900, 2100)]
    public int Anno { get; set; }

    public VehicleType Tipo { get; set; } = VehicleType.Unknown;
    public FuelType Carburante { get; set; } = FuelType.Unknown;

    [StringLength(30)]
    public string? Colore { get; set; }

    [StringLength(17)]
    public string? Vin { get; set; }

    public int KmAttuali { get; set; }
    public DateOnly? DataImmatricolazione { get; set; }
    public DateOnly? DataAcquisto { get; set; }
    public decimal? PrezzoAcquisto { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; }
}

// ── Modifica ──────────────────────────────────────────────────────────────────

public class UpdateVehicleDto : CreateVehicleDto
{
    public bool Attivo { get; set; } = true;
}