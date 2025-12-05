using CarManager.Api.DTOs.MaintenanceDTO;
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
                Type = maintenance.Type,
                Km = maintenance.Km,
                Workshop = maintenance.Workshop,
                Notes = maintenance.Notes
            };
        public static Maintenance ToEntity(this CreateMaintenanceDTO dto) =>
            new()
            {
                VehicleId = dto.VehicleId!.Value,                
                Date = dto.Date,
                Type = dto.Type,
                Description = dto.Description,
                Km = dto.Km,
                Cost = dto.Cost,
                Workshop = dto.Workshop,
                Notes = dto.Notes
            };
        public static void UpdateEntity(this UpdateMaintenanceDTO dto, Maintenance maintenance)
        {
            maintenance.VehicleId = dto.VehicleId;
            maintenance.Date = dto.Date;
            maintenance.Type = dto.Type;
            maintenance.Description = dto.Description;
            maintenance.Km = dto.Km;
            maintenance.Cost = dto.Cost;
            maintenance.Workshop = dto.Workshop;
            maintenance.Notes = dto.Notes;
        }
    }
}

