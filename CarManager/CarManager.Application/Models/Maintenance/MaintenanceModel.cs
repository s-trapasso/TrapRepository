using CarManager.Core.Enums;

namespace CarManager.Application.Models.Maintenance
{
    public class MaintenanceModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string? VehiclePlate { get; set; }
        public DateTime Date { get; set; }
        public MaintenanceType MaintenanceType { get; set; }
        public string? MaintenanceTypeName { get; set; }
        public string? Description { get; set; }
        public int? Km { get; set; }
        public decimal? Cost { get; set; }
        public string? Workshop { get; set; }
        public string? Notes { get; set; }
    }
}
