using Microsoft.EntityFrameworkCore;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Repositories;

public interface IOwnerRepository : IRepository<Owner>
{
    Task<IEnumerable<Owner>> GetAllWithVehiclesAsync();
    Task<Owner?> GetByIdWithVehiclesAsync(int id);
    Task<IEnumerable<Owner>> GetConPatenteInScadenzaAsync(int giorniPreavviso = 30);
    Task<IEnumerable<Owner>> GetConVisitaInScadenzaAsync(int giorniPreavviso = 30);
}

public class OwnerRepository : Repository<Owner>, IOwnerRepository
{
    public OwnerRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Owner>> GetAllWithVehiclesAsync()
        => await _context.Owners
            .Include(o => o.Veicoli)
                .ThenInclude(vo => vo.Vehicle)
            .AsNoTracking()
            .OrderBy(o => o.Cognome)
            .ThenBy(o => o.Nome)
            .ToListAsync();

    public async Task<Owner?> GetByIdWithVehiclesAsync(int id)
        => await _context.Owners
            .Include(o => o.Veicoli.OrderByDescending(vo => vo.DataAcquisto))
                .ThenInclude(vo => vo.Vehicle)
            .FirstOrDefaultAsync(o => o.Id == id);

    /// <summary>Owner con patente in scadenza entro N giorni.</summary>
    public async Task<IEnumerable<Owner>> GetConPatenteInScadenzaAsync(int giorniPreavviso = 30)
    {
        var soglia = DateOnly.FromDateTime(DateTime.Today.AddDays(giorniPreavviso));
        return await _context.Owners
            .AsNoTracking()
            .Where(o => o.PatenteScadenza.HasValue && o.PatenteScadenza.Value <= soglia)
            .OrderBy(o => o.PatenteScadenza)
            .ToListAsync();
    }

    /// <summary>Owner con visita medica in scadenza entro N giorni.</summary>
    public async Task<IEnumerable<Owner>> GetConVisitaInScadenzaAsync(int giorniPreavviso = 30)
    {
        var soglia = DateOnly.FromDateTime(DateTime.Today.AddDays(giorniPreavviso));
        return await _context.Owners
            .AsNoTracking()
            .Where(o => o.PatenteMedicaScadenza.HasValue && o.PatenteMedicaScadenza.Value <= soglia)
            .OrderBy(o => o.PatenteMedicaScadenza)
            .ToListAsync();
    }
}