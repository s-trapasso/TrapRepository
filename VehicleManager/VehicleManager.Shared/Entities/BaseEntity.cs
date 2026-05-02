namespace VehicleManager.Shared.Entities;

/// <summary>
/// Classe base per tutte le entità. Fornisce Id, timestamp di creazione e aggiornamento.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
