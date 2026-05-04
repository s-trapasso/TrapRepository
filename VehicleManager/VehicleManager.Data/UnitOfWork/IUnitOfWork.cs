namespace VehicleManager.Data.UnitOfWork;

using VehicleManager.Data.Repositories;

/// <summary>
/// Unit of Work: coordina tutti i repository e garantisce
/// che le operazioni su più entità vengano salvate in una sola transazione.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IVehicleRepository Vehicles { get; }
    IMaintenanceRepository Maintenances { get; }
    IOwnerRepository Owners { get; }
    IDeadlineRepository Deadlines { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}