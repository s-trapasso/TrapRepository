using Microsoft.EntityFrameworkCore;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Repositories;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Carica il veicolo con tutti i dettagli: manutenzioni, scadenze, proprietari e documenti.
    /// </summary>
    public async Task<Vehicle?> GetByIdWithDetailsAsync(int id)
        => await _context.Vehicles
            .Include(v => v.Manutenzioni.OrderByDescending(m => m.DataIntervento))
            .Include(v => v.Scadenze.OrderBy(d => d.DataScadenza))
            .Include(v => v.Proprietari.OrderByDescending(p => p.DataAcquisto))
                .ThenInclude(p => p.Owner)
            .Include(v => v.Documenti)
            .FirstOrDefaultAsync(v => v.Id == id);

    /// <summary>
    /// Carica tutti i veicoli con il proprietario attuale e i conteggi delle scadenze.
    /// Usato per la lista e la dashboard.
    /// </summary>
    public async Task<IEnumerable<Vehicle>> GetAllWithSummaryAsync()
        => await _context.Vehicles
            .Include(v => v.Proprietari.Where(p => p.DataCessione == null))
                .ThenInclude(p => p.Owner)
            .Include(v => v.Scadenze)
            .AsNoTracking()
            .OrderBy(v => v.Marca)
            .ThenBy(v => v.Modello)
            .ToListAsync();

    public async Task<Vehicle?> GetByTargaAsync(string targa)
        => await _context.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Targa.ToUpper() == targa.ToUpper());

    public async Task<IEnumerable<Vehicle>> GetAttiviAsync()
        => await _context.Vehicles
            .Include(v => v.Proprietari.Where(p => p.DataCessione == null))
                .ThenInclude(p => p.Owner)
            .Include(v => v.Scadenze)
            .AsNoTracking()
            .Where(v => v.Attivo)
            .OrderBy(v => v.Marca)
            .ToListAsync();

    public async Task<IEnumerable<Vehicle>> GetByOwnerAsync(int ownerId)
        => await _context.Vehicles
            .Include(v => v.Proprietari)
            .AsNoTracking()
            .Where(v => v.Proprietari.Any(p => p.OwnerId == ownerId))
            .OrderBy(v => v.Marca)
            .ToListAsync();
}