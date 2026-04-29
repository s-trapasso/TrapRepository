using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarManager.Api.Common;
using CarManager.Api.Data;
using CarManager.Api.DTOs.Owner;
using CarManager.Api.DTOs.Vehicle;
using CarManager.Api.Mappings;
using CarManager.Api.Services.Interfaces;
using CarManager.Core.Enums;
using CarManager.Core.Models;
using CodiceFiscaleLib.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly CarManagerDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<OwnerService> _logger;
        public OwnerService(CarManagerDbContext db, IMapper mapper, ILogger<OwnerService> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        #region GET ALL
        public async Task<List<OwnerDTO>> GetAllAsync()
        {
            return await _db.Owners
                .ProjectTo<OwnerDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        #endregion

        #region GET BY ID

        public async Task<OwnerDTO?> GetByIdAsync(int id)
        {
            return await _db.Owners
            .Where(o => o.Id == id)
            .ProjectTo<OwnerDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        }

        #endregion

        #region GET WITH VEHICLES

        public async Task<OwnerDTO?> GetWithVehiclesAsync(int id)
        {
            return await _db.Owners
           .Where(o => o.Id == id)
           .ProjectTo<OwnerDTO>(_mapper.ConfigurationProvider)
           .FirstOrDefaultAsync();
        }

        #endregion

        #region SEARCH

        public async Task<List<OwnerDTO>> SearchAsync(string name)
        {
            var term = name.Trim();

            return await _db.Owners
                .Where(o => o.FirstName.Contains(term) || o.LastName.Contains(term))
                .ProjectTo<OwnerDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        #endregion

        #region CREATE

        public async Task<Result<OwnerDTO>> CreateAsync(CreateOwnerDTO dto)
        {
            // 1. validazione base (opzionale ma utile)
            var exists = await _db.Owners
                .AnyAsync(o => o.FiscalCode == dto.FiscalCode);

            if (exists)
                return Result<OwnerDTO>.Fail(ErrorCode.DuplicateFiscalCode);

            // 2. mapping base
            var entity = _mapper.Map<Owner>(dto);

            // 3. BUSINESS LOGIC: generazione codice fiscale
            try
            {
                entity.FiscalCode = CodiceFiscaleLib.Helpers.EncodingHelper.Encode(
                    dto.LastName,
                    dto.FirstName,
                    dto.Gender == Core.Enums.OwnerGender.Male ? 'M' : 'F',
                    dto.BirthDate,
                    dto.BirthPlace
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore generazione codice fiscale per {@Dto}", dto);
                return Result<OwnerDTO>.Fail(ErrorCode.InvalidFiscalCode);
            }

            // 4. save
            _db.Owners.Add(entity);
            await _db.SaveChangesAsync();

            // 5. projection finale coerente (NO ToDto)
            var result = await _db.Owners
                .Where(o => o.Id == entity.Id)
                .ProjectTo<OwnerDTO>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Result<OwnerDTO>.Ok(result);
        }

        #endregion

        #region UPDATE

        public async Task<Result<OwnerDTO>> UpdateAsync(int id, UpdateOwnerDTO dto)
        {
            var entity = await _db.Owners.FindAsync(id);

            if (entity == null)
                return Result<OwnerDTO>.Fail(ErrorCode.NotFound);

            _mapper.Map(dto, entity);

            await _db.SaveChangesAsync();

            var result = await _db.Owners
                .Where(o => o.Id == id)
                .ProjectTo<OwnerDTO>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Result<OwnerDTO>.Ok(result);
        }

        #endregion

        #region DELETE

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var entity = await _db.Owners.FindAsync(id);

            if (entity == null)
                return Result<bool>.Fail(ErrorCode.NotFound);

            _db.Owners.Remove(entity);
            await _db.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        #endregion
    }
}
