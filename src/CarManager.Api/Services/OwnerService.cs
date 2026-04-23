using CarManager.Api.Data;
using CarManager.Api.DTOs.Owner;
using CarManager.Api.DTOs.Vehicle;
using CarManager.Api.Mappings;
using CarManager.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly CarManagerDbContext _db;

        public OwnerService(CarManagerDbContext db)
        {
            _db = db;
        }

        #region GET ALL

        public async Task<List<OwnerDTO>> GetAllAsync()
        {
            return await _db.Owners
                .AsNoTracking()
                .OrderBy(o => o.LastName)
                .Select(o => o.ToDto())
                .ToListAsync();
        }

        #endregion

        #region GET BY ID

        public async Task<OwnerDTO?> GetByIdAsync(int id)
        {
            var owner = await _db.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            return owner?.ToDto();
        }

        #endregion

        #region GET WITH VEHICLES (RELATION IMPORTANT)

        public async Task<OwnerDTO?> GetWithVehiclesAsync(int id)
        {
            var owner = await _db.Owners
                .Include(o => o.Vehicles)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (owner == null)
                return null;

            var dto = owner.ToDto();

            // aggiunta manuale veicoli (senza creare dipendenze circolari)
            dto.Vehicles = owner.Vehicles
                .Select(v => new VehicleDTO
                {
                    Id = v.Id,
                    Plate = v.Plate,
                    Brand = v.Brand,
                    Model = v.Model,
                    Year = v.Year,
                    Km = v.Km,
                    FuelType = v.FuelType
                })
                .ToList();

            return dto;
        }

        #endregion

        #region CREATE

        public async Task<(bool Success, string? Error, OwnerDTO? Data)> CreateAsync(CreateOwnerDTO dto)
        {
            var exists = await _db.Owners
                .AnyAsync(o => o.FiscalCode == dto.FiscalCode);

            if (exists)
                return (false, "DuplicateFiscalCode", null);

            var entity = dto.ToEntity();

            _db.Owners.Add(entity);
            await _db.SaveChangesAsync();

            return (true, null, entity.ToDto());
        }

        #endregion

        #region UPDATE

        public async Task<(bool Success, string? Error)> UpdateAsync(int id, UpdateOwnerDTO dto)
        {
            var owner = await _db.Owners.FindAsync(id);

            if (owner == null)
                return (false, "NotFound");

            owner.UpdateFrom(dto);

            await _db.SaveChangesAsync();

            return (true, null);
        }

        #endregion

        #region DELETE

        public async Task<bool> DeleteAsync(int id)
        {
            var owner = await _db.Owners
                .Include(o => o.Vehicles)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (owner == null)
                return false;

            // ⚠️ gestione relazione
            if (owner.Vehicles.Any())
                return false; // oppure business rule: non cancellabile

            _db.Owners.Remove(owner);
            await _db.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
