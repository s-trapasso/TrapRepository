using Microsoft.EntityFrameworkCore.Storage;
using VehicleManager.Data.Repositories;

namespace VehicleManager.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    // Repository lazy: vengono creati solo quando servono
    private IVehicleRepository? _vehicles;
    private IMaintenanceRepository? _maintenances;
    private IOwnerRepository? _owners;
    private IDeadlineRepository? _deadlines;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IVehicleRepository Vehicles => _vehicles ??= new VehicleRepository(_context);
    public IMaintenanceRepository Maintenances => _maintenances ??= new MaintenanceRepository(_context);
    public IOwnerRepository Owners => _owners ??= new OwnerRepository(_context);
    public IDeadlineRepository Deadlines => _deadlines ??= new DeadlineRepository(_context);

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task BeginTransactionAsync()
        => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}