namespace CarManager.Api.DTOs
{
    public class DashboardQuery
    {
        public int? OwnerId { get; set; }
        public int? VehicleId { get; set; }

        // finestra temporale
        public int MaintenanceMonthsBack { get; set; } = 6;

        
    }
}
