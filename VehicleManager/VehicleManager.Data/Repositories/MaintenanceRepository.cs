using Microsoft.EntityFrameworkCore;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Repositories;

public interface IMaintenanceRepository : IRepository<Maintenance>
{
    Task<IEnumerable<Maintenance>> GetByVehicleAsync(int vehicleId);
    Task<Maintenance?> GetByIdWithDocumentsAsync(int id);
    Task<IEnumerable<Maintenance>> GetUltimeNAsync(int vehicleId, int n = 5);
    Task<decimal> GetTotaleCostiAsync(int vehicleId);
}

public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
{
    public MaintenanceRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Maintenance>> GetByVehicleAsync(int vehicleId)
        => await _context.Maintenances
            .AsNoTracking()
            .Where(m => m.VehicleId == vehicleId)
            .OrderByDescending(m => m.DataIntervento)
            .ToListAsync();

    public async Task<Maintenance?> GetByIdWithDocumentsAsync(int id)
        => await _context.Maintenances
            .Include(m => m.Documenti)
            .FirstOrDefaultAsync(m => m.Id == id);

    /// <summary>Ultime N manutenzioni di un veicolo (per la scheda veicolo).</summary>
    public async Task<IEnumerable<Maintenance>> GetUltimeNAsync(int vehicleId, int n = 5)
        => await _context.Maintenances
            .AsNoTracking()
            .Where(m => m.VehicleId == vehicleId)
            .OrderByDescending(m => m.DataIntervento)
            .Take(n)
            .ToListAsync();

    /// <summary>Totale costi manutenzioni di un veicolo.</summary>
    public async Task<decimal> GetTotaleCostiAsync(int vehicleId)
        => await _context.Maintenances
            .Where(m => m.VehicleId == vehicleId && m.Costo.HasValue)
            .SumAsync(m => m.Costo!.Value);
}