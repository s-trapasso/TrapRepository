using CarManager.Api.DTOs.OwnerDTO;
using CarManager.Core.Models;

namespace CarManager.Api.Mappings
{
    public static class OwnerMappings
    {
        public static OwnerDTO ToDto(this Owner owner)
       => new()
       {
              Id = owner.Id,
              FirstName = owner.FirstName,
              LastName = owner.LastName,
              Address = owner.Address,
              BirthDate = owner.BirthDate,
              BirthPlace = owner.BirthPlace,
              Gender = owner.Gender.ToString(),
              FiscalCode = owner.FiscalCode,

       };

        public static Owner ToEntity(this CreateOwnerDTO dto)
            => new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                BirthDate = dto.BirthDate,
                BirthPlace = dto.BirthPlace,
                Gender = dto.Gender,
                FiscalCode = dto.FiscalCode,

            };

        public static void UpdateEntity(this UpdateOwnerDTO dto, Owner owner)
        {
            owner.FirstName = dto.FirstName;
            owner.LastName = dto.LastName;
            owner.Address = dto.Address;
            owner.BirthDate = dto.BirthDate;
            owner.BirthPlace = dto.BirthPlace;
            owner.Gender = dto.Gender;
            owner.FiscalCode = dto.FiscalCode;
        }
        public static void UpdateFrom(this Owner owner, UpdateOwnerDTO dto)
        {
            owner.FirstName = dto.FirstName;
            owner.LastName = dto.LastName;
            owner.Address = dto.Address;
            owner.BirthDate = dto.BirthDate;
            owner.BirthPlace = dto.BirthPlace;
            owner.Gender = dto.Gender;
            owner.FiscalCode = dto.FiscalCode;
        }
    }
}
