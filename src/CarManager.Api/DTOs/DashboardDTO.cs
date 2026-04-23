using CarManager.Api.DTOs;
using CarManager.Api.DTOs.Maintenance;
using CarManager.Api.DTOs.Vehicle;

namespace CarManager.Api.DTOs
{
    public class DashboardDTO
    {
        public int TotalVehicles { get; set; }
        public int TotalOwners { get; set; }
        public int TotalMaintenances { get; set; }

        public decimal TotalMaintenanceCost { get; set; }
        public decimal AvgCostPerVehicle { get; set; }

        public List<MaintenanceDTO> LastMaintenances { get; set; } = new();

        public List<VehicleDTO> VehiclesWithoutRecentMaintenance { get; set; } = new();

        public List<AlertDTO> Alerts { get; set; } = new();

        public Dictionary<string, int> MaintenanceByType { get; set; } = new();

        public Dictionary<string, int> VehiclesByFuelType { get; set; } = new();
    }
}
