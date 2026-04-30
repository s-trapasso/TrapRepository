using CarManager.Core.Enums;
namespace CarManager.Application.Models.Vehicle
{
    public class VehicleModel
    {
        public int Id { get; set; }

        public string Plate { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string BrandModel => $"{Brand} {Model}";

        public FuelTypeEnum FuelType { get; set; }
        public string FuelTypeName { get; set; } = string.Empty;

        public int Km { get; set; }

        public TireType CurrentTireType { get; set; }
        public string CurrentTireTypeName { get; set; } = string.Empty;

        public DateTime? LastTireChangeDate { get; set; }

        public int OwnerId { get; set; }
        public string? OwnerDisplay { get; set; }
    }
}
