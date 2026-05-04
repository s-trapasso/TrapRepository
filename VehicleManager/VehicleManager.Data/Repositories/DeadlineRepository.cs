using Microsoft.EntityFrameworkCore;
using VehicleManager.Shared.Entities;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Data.Repositories;

public interface IDeadlineRepository : IRepository<Deadline>
{
    Task<IEnumerable<Deadline>> GetByVehicleAsync(int vehicleId);
    Task<IEnumerable<Deadline>> GetInScadenzaAsync(int giorniPreavviso = 30);
    Task<IEnumerable<Deadline>> GetScaduteAsync();
    Task<IEnumerable<Deadline>> GetAllWithVehicleAsync();
}

public class DeadlineRepository : Repository<Deadline>, IDeadlineRepository
{
    public DeadlineRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Deadline>> GetByVehicleAsync(int vehicleId)
        => await _context.Deadlines
            .AsNoTracking()
            .Where(d => d.VehicleId == vehicleId)
            .OrderBy(d => d.DataScadenza)
            .ToListAsync();

    /// <summary>Tutte le scadenze entro N giorni (per la dashboard alert).</summary>
    public async Task<IEnumerable<Deadline>> GetInScadenzaAsync(int giorniPreavviso = 30)
    {
        var oggi = DateOnly.FromDateTime(DateTime.Today);
        var soglia = DateOnly.FromDateTime(DateTime.Today.AddDays(giorniPreavviso));
        return await _context.Deadlines
            .Include(d => d.Vehicle)
            .AsNoTracking()
            .Where(d => d.DataScadenza >= oggi && d.DataScadenza <= soglia)
            .OrderBy(d => d.DataScadenza)
            .ToListAsync();
    }

    /// <summary>Scadenze già scadute (per alert dashboard).</summary>
    public async Task<IEnumerable<Deadline>> GetScaduteAsync()
    {
        var oggi = DateOnly.FromDateTime(DateTime.Today);
        return await _context.Deadlines
            .Include(d => d.Vehicle)
            .AsNoTracking()
            .Where(d => d.DataScadenza < oggi)
            .OrderBy(d => d.DataScadenza)
            .ToListAsync();
    }

    public async Task<IEnumerable<Deadline>> GetAllWithVehicleAsync()
        => await _context.Deadlines
            .Include(d => d.Vehicle)
            .AsNoTracking()
            .OrderBy(d => d.DataScadenza)
            .ToListAsync();
}