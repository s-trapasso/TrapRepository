using CarManager.Core.Enums;

namespace CarManager.Application.Models.Vehicle
{
    public class VehicleCreateModel
    {
        public string Plate { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }
        public int Km { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public int OwnerId { get; set; }
    }
}
