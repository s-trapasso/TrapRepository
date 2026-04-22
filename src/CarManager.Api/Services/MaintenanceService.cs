using CarManager.Api;
using CarManager.Api.Data;
using CarManager.Api.DTOs.MaintenanceDTO;
using CarManager.Api.DTOs.VehicleDTO;
using CarManager.Api.Mappings;
using CarManager.Api.Services.Interfaces;
using CarManager.Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly CarManagerDbContext _db;

        public MaintenanceService(CarManagerDbContext db)
        {
            _db = db;
        }

        public async Task<List<MaintenanceDTO>> GetAllAsync()
        {
            return await _db.Maintenances
                .Include(m => m.Vehicle)
                .OrderByDescending(m => m.Date)
                .Select(m => m.ToDto())
                .ToListAsync();
        }

        public async Task<List<MaintenanceDTO>> GetByVehicleIdAsync(int vehicleId)
        {
            return await _db.Maintenances
                .Where(m => m.VehicleId == vehicleId)
                .OrderByDescending(m => m.Date)
                .Select(m => m.ToDto())
                .ToListAsync();
        }

        public async Task<MaintenanceDTO?> GetByIdAsync(int id)
        {
            var entity = await _db.Maintenances
                .Include(m => m.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);

            return entity?.ToDto();
        }

        public async Task<(bool Success, string? Error, MaintenanceDTO? Data)> CreateAsync(CreateMaintenanceDTO dto)
        {
            var vehicle = await _db.Vehicles.FindAsync(dto.VehicleId);

            if (vehicle == null)
                return (false, "VehicleNotFound", null);

            var entity = dto.ToEntity();

            _db.Maintenances.Add(entity);
            await _db.SaveChangesAsync();

            // reload vehicle plate (opzionale ma utile per DTO)
            await _db.Entry(entity).Reference(x => x.Vehicle).LoadAsync();

            return (true, null, entity.ToDto());
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(int id, UpdateMaintenanceDTO dto)
        {
            var entity = await _db.Maintenances.FindAsync(id);

            if (entity == null)
                return (false, "NotFound");

            dto.UpdateEntity(entity);

            await _db.SaveChangesAsync();

            return (true, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Maintenances.FindAsync(id);

            if (entity == null)
                return false;

            _db.Maintenances.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
