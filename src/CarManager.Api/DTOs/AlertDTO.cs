namespace CarManager.Api.DTOs
{
    public class AlertDTO
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Severity { get; set; } = "Info"; // Info / Warning / Critical

        public int? VehicleId { get; set; }
        public string? Plate { get; set; }
    }
}
