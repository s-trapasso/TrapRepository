using CarManager.Api;
using CarManager.Api.Data;
using CarManager.Api.DTOs.VehicleDTO;
using CarManager.Api.Mappings;
using CarManager.Api.Services.Interfaces;
using CarManager.Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly CarManagerDbContext _db;

        public VehicleService(CarManagerDbContext db)
        {
            _db = db;
        }
        public async Task<(bool Success, VehicleError Error, VehicleDTO? Vehicle)> CreateAsync(CreateVehicleDTO dto)
        {
            var exists = await _db.Vehicles
                .AnyAsync(v => v.Plate.ToLower() == dto.Plate.ToLower());

            if (exists)
                return (false, VehicleError.DuplicatePlate, null);

            var vehicle = dto.ToEntity();

            _db.Vehicles.Add(vehicle);
            await _db.SaveChangesAsync();

            return (true, VehicleError.None, vehicle.ToDto());
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle == null)
                return false;

            _db.Vehicles.Remove(vehicle);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<VehicleDTO>> GetAllAsync()
        {
            var vehicles = await _db.Vehicles
            .AsNoTracking()
            .OrderBy(v => v.Plate)
            .ToListAsync();

            return vehicles.Select(v => v.ToDto()).ToList();
        }

        public async Task<VehicleDTO?> GetByIdAsync(int id)
        {
            var vehicle = await _db.Vehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

            return vehicle?.ToDto();
        }

        public async Task<List<VehicleDTO>> SearchByPlateAsync(string plate)
        {
            var term = plate.Trim().ToLower();

            var vehicles = await _db.Vehicles
                .AsNoTracking()
                .Where(v => v.Plate.ToLower().Contains(term))
                .ToListAsync();

            return vehicles.Select(v => v.ToDto()).ToList();
        }

        public async Task<(bool Success, VehicleError Error)> UpdateAsync(int id, UpdateVehicleDTO dto)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle == null)
                return (false, VehicleError.NotFound);

            var exists = await _db.Vehicles
                .AnyAsync(v => v.Id != id);

            if (exists)
                return (false, VehicleError.DuplicatePlate);

            dto.UpdateEntity(vehicle);
            await _db.SaveChangesAsync();

            return (true, VehicleError.None);
        }
    }
}
