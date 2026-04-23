using CarManager.Api.DTOs;
using CarManager.Api.DTOs.MaintenanceDTO;
using CarManager.Core.Enums;
using CarManager.Core.Models;

namespace CarManager.Api.Mappings
{
    public static class MaintenanceMappings
    {
        public static MaintenanceDTO ToDto(this Maintenance maintenance)
            => new()
            {
                Id = maintenance.Id,
                VehicleId = maintenance.VehicleId,
                VehiclePlate = maintenance.Vehicle?.Plate,               
                Description = maintenance.Description,
                Date = maintenance.Date,
                Cost = maintenance.Cost,
                MaintenanceType = maintenance.MaintenanceType,
                Km = maintenance.Km,
                Workshop = maintenance.Workshop,
                Notes = maintenance.Notes
            };
        public static Maintenance ToEntity(this CreateMaintenanceDTO dto) =>
            new()
            {
                VehicleId = dto.VehicleId,                
                Date = dto.Date,
                MaintenanceType = dto.MaintenanceType,
                Description = dto.Description,
                Km = dto.Km,
                Cost = dto.Cost,
                Workshop = dto.Workshop,
                Notes = dto.Notes
            };
        public static void UpdateEntity(this UpdateMaintenanceDTO dto, Maintenance maintenance)
        {
            maintenance.Date = dto.Date;
            maintenance.MaintenanceType = dto.MaintenanceType;
            maintenance.Description = dto.Description;
            maintenance.Km = dto.Km;
            maintenance.Cost = dto.Cost;
            maintenance.Workshop = dto.Workshop;
            maintenance.Notes = dto.Notes;
        }
        public static Maintenance ToTireChangeEntity(this TireChangeDTO dto, TireType oldType)
        {
            return new Maintenance
            {
                VehicleId = dto.VehicleId,
                Date = dto.Date,
                MaintenanceType = MaintenanceType.TireChange,
                Description = $"Cambio gomme {oldType} → {dto.NewTireType}",
                Km = dto.Km,
                Notes = dto.Notes
            };
        }
    }
}

