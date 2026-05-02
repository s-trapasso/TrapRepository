using System.ComponentModel.DataAnnotations;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Shared.DTOs;

// ═════════════════════════════════════════════════════════════════════════════
// MAINTENANCE
// ═════════════════════════════════════════════════════════════════════════════

public class MaintenanceDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public string VehicleLabel { get; set; } = string.Empty;   // es. "Toyota Yaris AB123CD"
    public MaintenanceType Tipo { get; set; }
    public string TipoLabel => Tipo.ToString();
    public string Descrizione { get; set; } = string.Empty;
    public DateOnly DataIntervento { get; set; }
    public int? KmAlMomento { get; set; }
    public int? KmProssimoIntervento { get; set; }
    public DateOnly? DataProssimoIntervento { get; set; }
    public decimal? Costo { get; set; }
    public string? Officina { get; set; }
    public string? NumeroFattura { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateMaintenanceDto
{
    [Required]
    public int VehicleId { get; set; }

    public MaintenanceType Tipo { get; set; }

    [Required]
    [StringLength(200)]
    public string Descrizione { get; set; } = string.Empty;

    public DateOnly DataIntervento { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public int? KmAlMomento { get; set; }
    public int? KmProssimoIntervento { get; set; }
    public DateOnly? DataProssimoIntervento { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Costo { get; set; }

    [StringLength(200)]
    public string? Officina { get; set; }

    [StringLength(50)]
    public string? NumeroFattura { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; }
}

public class UpdateMaintenanceDto : CreateMaintenanceDto { }

// ═════════════════════════════════════════════════════════════════════════════
// OWNER
// ═════════════════════════════════════════════════════════════════════════════

public class OwnerDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cognome { get; set; } = string.Empty;
    public string NomeCompleto => $"{Nome} {Cognome}";
    public string? CodiceFiscale { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Indirizzo { get; set; }
    public string? Note { get; set; }
    public int NumeroVeicoli { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateOwnerDto
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
}

public class UpdateOwnerDto : CreateOwnerDto { }

// ═════════════════════════════════════════════════════════════════════════════
// VEHICLE OWNERSHIP
// ═════════════════════════════════════════════════════════════════════════════

public class VehicleOwnershipDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int OwnerId { get; set; }
    public string NomeProprietario { get; set; } = string.Empty;
    public OwnershipType TipoProprietà { get; set; }
    public DateOnly DataAcquisto { get; set; }
    public DateOnly? DataCessione { get; set; }
    public decimal? PrezzoAcquisto { get; set; }
    public decimal? PrezzoVendita { get; set; }
    public int? KmAlAcquisto { get; set; }
    public int? KmAllaCessione { get; set; }
    public string? Note { get; set; }
    public bool IsAttuale { get; set; }
}

public class CreateVehicleOwnershipDto
{
    [Required] public int VehicleId { get; set; }
    [Required] public int OwnerId { get; set; }
    public OwnershipType TipoProprietà { get; set; } = OwnershipType.Proprietario;
    public DateOnly DataAcquisto { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly? DataCessione { get; set; }
    public decimal? PrezzoAcquisto { get; set; }
    public decimal? PrezzoVendita { get; set; }
    public int? KmAlAcquisto { get; set; }
    public int? KmAllaCessione { get; set; }
    [StringLength(500)] public string? Note { get; set; }
}

// ═════════════════════════════════════════════════════════════════════════════
// DEADLINE
// ═════════════════════════════════════════════════════════════════════════════

public class DeadlineDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public string VehicleLabel { get; set; } = string.Empty;
    public DeadlineType Tipo { get; set; }
    public string TipoLabel => Tipo.ToString();
    public string? Descrizione { get; set; }
    public DateOnly DataScadenza { get; set; }
    public DateOnly? DataRinnovo { get; set; }
    public decimal? Costo { get; set; }
    public string? Compagnia { get; set; }
    public string? NumeroPolizza { get; set; }
    public bool NotificaAttiva { get; set; }
    public int GiorniPreavviso { get; set; }
    public string? Note { get; set; }

    // Calcolati server-side
    public int GiorniAllaScadenza { get; set; }
    public DeadlineStatus Stato { get; set; }
    public string StatoLabel => Stato.ToString();
}

public class CreateDeadlineDto
{
    [Required] public int VehicleId { get; set; }
    public DeadlineType Tipo { get; set; }
    [StringLength(200)] public string? Descrizione { get; set; }
    public DateOnly DataScadenza { get; set; }
    public DateOnly? DataRinnovo { get; set; }
    [Range(0, double.MaxValue)] public decimal? Costo { get; set; }
    [StringLength(200)] public string? Compagnia { get; set; }
    [StringLength(100)] public string? NumeroPolizza { get; set; }
    public bool NotificaAttiva { get; set; } = true;
    public int GiorniPreavviso { get; set; } = 30;
    [StringLength(500)] public string? Note { get; set; }
}

public class UpdateDeadlineDto : CreateDeadlineDto { }