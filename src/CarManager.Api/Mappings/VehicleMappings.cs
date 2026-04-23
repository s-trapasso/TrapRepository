using CarManager.Api.DTOs.Vehicle;
using CarManager.Core.Enums;
using CarManager.Core.Models;
using System.Security.Cryptography;

namespace CarManager.Api.Mappings;

public static class VehicleMappings
{
    public static VehicleDTO ToDto(this Vehicle vehicle)
        => new()
        {
            Id = vehicle.Id,
            Plate = vehicle.Plate,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            Year = vehicle.Year,
            Km = vehicle.Km,
            FuelType = vehicle.FuelType
        };

    public static Vehicle ToEntity(this CreateVehicleDTO dto)
        => new()
        {
            Plate = dto.Plate,
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            Km = dto.Km,
            FuelType = dto.FuelType,
            OwnerId = dto.OwnerId,
            CurrentTireType = TireType.AllSeason,
            LastTireChangeDate = null

        };

    public static void UpdateEntity(this UpdateVehicleDTO dto, Vehicle vehicle)
    {
        vehicle.Brand = dto.Brand;
        vehicle.Model = dto.Model;
        vehicle.Year = dto.Year;
        vehicle.Km = dto.Km;
        vehicle.FuelType = dto.FuelType;
    }
}
