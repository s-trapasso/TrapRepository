using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarManager.Api;
using CarManager.Api.Common;
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
        private readonly IMapper _mapper;
        public MaintenanceService(CarManagerDbContext db, ILogger<MaintenanceService> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<MaintenanceDTO>> GetAllAsync()
        {
            return await _db.Maintenances
                            .ProjectTo<MaintenanceDTO>(_mapper.ConfigurationProvider) // 👈 usa AutoMapper per proiettare direttamente in DTO
                            .ToListAsync();
        }

        public async Task<List<MaintenanceDTO>> GetByVehicleIdAsync(int vehicleId)
        {
            return await _db.Maintenances
                            .Where(m => m.VehicleId == vehicleId)
                            .ProjectTo<MaintenanceDTO>(_mapper.ConfigurationProvider) // 👈 usa AutoMapper per proiettare direttamente in DTO
                            .ToListAsync();
        }

        public async Task<MaintenanceDTO?> GetByIdAsync(int id)
        {
            return await _db.Maintenances
                            .Where(m => m.Id == id)
                            .ProjectTo<MaintenanceDTO>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();
        }

        public async Task<Result<MaintenanceDTO>> CreateAsync(CreateMaintenanceDTO dto)
        {
            var vehicleExists = await _db.Vehicles.AnyAsync(v => v.Id == dto.VehicleId);

            if (!vehicleExists)
                return Result<MaintenanceDTO>.Fail(ErrorCode.VehicleNotFound);

            var entity = _mapper.Map<Maintenance>(dto);

            _db.Maintenances.Add(entity);
            await _db.SaveChangesAsync();

            var result = await _db.Maintenances
                                .Where(m => m.Id == entity.Id)
                                .ProjectTo<MaintenanceDTO>(_mapper.ConfigurationProvider)
                                .FirstAsync();

            return Result<MaintenanceDTO>.Ok(result);
        }

        public async Task<Result<MaintenanceDTO>> UpdateAsync(int id, UpdateMaintenanceDTO dto)
        {
            var entity = await _db.Maintenances.FindAsync(id);

            if (entity == null)
                return Result<MaintenanceDTO>.Fail(ErrorCode.NotFound);

            _mapper.Map(dto, entity);

            await _db.SaveChangesAsync();

            var result = await _db.Maintenances
                                .Where(m => m.Id == id)
                                .ProjectTo<MaintenanceDTO>(_mapper.ConfigurationProvider)
                                .FirstAsync();

            return Result<MaintenanceDTO>.Ok(result);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var entity = await _db.Maintenances.FindAsync(id);

            if (entity == null)
                return Result<bool>.Fail(ErrorCode.NotFound);

            _db.Maintenances.Remove(entity);
            await _db.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
        public async Task<Result<bool>> RegisterTireChangeAsync(TireChangeDTO dto)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            var vehicle = await _db.Vehicles
                .FirstOrDefaultAsync(v => v.Id == dto.VehicleId);

            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle {Id} not found for tire change", dto.VehicleId);
                return Result<bool>.Fail(ErrorCode.VehicleNotFound);
            }

            // 1. aggiorno stato veicolo
            var oldType = vehicle.CurrentTireType;

            //UPDATE VEHICLE
            vehicle.CurrentTireType = dto.NewTireType;
            vehicle.LastTireChangeDate = dto.Date;

            // 2. creo maintenance storica
            var maintenance = _mapper.Map<Maintenance>(dto);

            _db.Maintenances.Add(maintenance);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation(
                "Tire change for vehicle {Id}: {Old} → {New}",
                vehicle.Id, oldType, dto.NewTireType
            );

            return Result<bool>.Ok(true);
        }
    }
}
