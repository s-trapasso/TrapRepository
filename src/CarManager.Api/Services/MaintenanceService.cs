using CarManager.Api;
using CarManager.Api.Data;
using CarManager.Api.DTOs;
using CarManager.Api.DTOs.Maintenance;
using CarManager.Api.DTOs.Vehicle;
using CarManager.Api.Mappings;
using CarManager.Api.Services.Interfaces;
using CarManager.Core.Enums;
using CarManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly CarManagerDbContext _db;
        private readonly ILogger<MaintenanceService> _logger;

        public MaintenanceService(CarManagerDbContext db, ILogger<MaintenanceService> logger)
        {
            _db = db;
            _logger = logger;
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
        public async Task<bool> RegisterTireChangeAsync(TireChangeDTO dto)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            var vehicle = await _db.Vehicles
                .FirstOrDefaultAsync(v => v.Id == dto.VehicleId);

            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle {Id} not found for tire change", dto.VehicleId);
                return false;
            }

            // 1. aggiorno stato veicolo
            var oldType = vehicle.CurrentTireType;

            vehicle.CurrentTireType = dto.NewTireType;
            vehicle.LastTireChangeDate = dto.Date;

            // 2. creo maintenance storica
            var maintenance = new Maintenance
            {
                VehicleId = vehicle.Id,
                Date = dto.Date,
                MaintenanceType = MaintenanceType.TireChange,
                Description = $"Cambio gomme {oldType} → {dto.NewTireType}",
                Km = dto.Km,
                Notes = dto.Notes
            };

            _db.Maintenances.Add(maintenance);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation(
                "Tire change for vehicle {Id}: {Old} → {New}",
                vehicle.Id, oldType, dto.NewTireType
            );

            return true;
        }
    }
}
