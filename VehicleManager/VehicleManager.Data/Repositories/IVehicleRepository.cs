using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByIdWithDetailsAsync(int id);          // con manutenzioni, scadenze, proprietari
    Task<IEnumerable<Vehicle>> GetAllWithSummaryAsync();     // con proprietario attuale e conteggio scadenze
    Task<Vehicle?> GetByTargaAsync(string targa);
    Task<IEnumerable<Vehicle>> GetAttiviAsync();
    Task<IEnumerable<Vehicle>> GetByOwnerAsync(int ownerId);
}