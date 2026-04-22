using CarManager.Core.Enum;


namespace CarManager.Api.DTOs.VehicleDTO
{
    public class VehicleDTO
    {
        public int Id { get; set; }

        public string Plate { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public FuelTypeEnum FuelType { get; set; }

        public int Km { get; set; }
    }
}
