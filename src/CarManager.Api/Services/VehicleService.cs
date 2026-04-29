using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarManager.Api;
using CarManager.Api.Common;
using CarManager.Api.Data;
using CarManager.Api.DTOs.Vehicle;
using CarManager.Api.Mappings;
using CarManager.Api.Services.Interfaces;
using CarManager.Core.Enums;
using CarManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly CarManagerDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleService> _logger;
        public VehicleService(CarManagerDbContext db, IMapper mapper, ILogger<VehicleService> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }



        // =========================
        // GET ALL
        // =========================
        public async Task<List<VehicleDTO>> GetAllAsync()
        {
            return await _db.Vehicles
                .ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // =========================
        // SEARCH
        // =========================
        public async Task<List<VehicleDTO>> SearchByPlateAsync(string plate)
        {
            return await _db.Vehicles
                .Where(v => v.Plate.Contains(plate))
                .ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<VehicleDTO?> GetByIdAsync(int id)
        {
            return await _db.Vehicles
                .Where(v => v.Id == id)
                .ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Result<VehicleDTO>> CreateAsync(CreateVehicleDTO dto)
        {
            var exists = await _db.Vehicles
                .AnyAsync(v => v.Plate == dto.Plate);

            if (exists)
                return Result<VehicleDTO>.Fail(ErrorCode.DuplicatePlate);

            var entity = _mapper.Map<Vehicle>(dto);

            _db.Vehicles.Add(entity);
            await _db.SaveChangesAsync();

            var result = await _db.Vehicles
                .Where(v => v.Id == entity.Id)
                .ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Result<VehicleDTO>.Ok(result);
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Result<VehicleDTO>> UpdateAsync(int id, UpdateVehicleDTO dto)
        {
            var entity = await _db.Vehicles.FindAsync(id);

            if (entity == null)
                return Result<VehicleDTO>.Fail(ErrorCode.NotFound);

            _mapper.Map(dto, entity);

            await _db.SaveChangesAsync();

            var result = await _db.Vehicles
                .Where(v => v.Id == id)
                .ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Result<VehicleDTO>.Ok(result);
        }

        // =========================
        // DELETE
        // =========================
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var entity = await _db.Vehicles.FindAsync(id);

            if (entity == null)
                return Result<bool>.Fail(ErrorCode.NotFound);

            _db.Vehicles.Remove(entity);
            await _db.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
