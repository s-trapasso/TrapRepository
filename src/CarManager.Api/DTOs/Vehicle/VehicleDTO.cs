using CarManager.Core.Enums;
using CarManager.Core.Extensions;


namespace CarManager.Api.DTOs.Vehicle
{
    public class VehicleDTO
    {
        public int Id { get; set; }

        public string Plate { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public FuelTypeEnum FuelType { get; set; }
        public string FuelTypeName => FuelType.GetDescription();

        public int Km { get; set; }
    }
}
