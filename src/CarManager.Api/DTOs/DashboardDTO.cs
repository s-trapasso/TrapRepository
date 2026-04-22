namespace CarManager.Api.DTOs
{
    public class DashboardDTO
    {
        public int TotalVehicles { get; set; }
        public int TotalOwners { get; set; }
        public int TotalMaintenances { get; set; }

        public decimal TotalMaintenanceCost { get; set; }

        public List<MaintenanceDTO.MaintenanceDTO> LastMaintenances { get; set; } = new();

        public List<VehicleDTO.VehicleDTO> VehiclesWithoutRecentMaintenance { get; set; } = new();
    }
}
