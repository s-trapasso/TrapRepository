using CarManager.App.Models.Vehicle;

namespace CarManager.App.Components.MappingsUI
{
    public static class VehicleUiMappings
    {
        public static VehicleCreateModel ToCreateModel(this VehicleModel dto)
            => new()
            {
                Plate = dto.Plate,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                Km = dto.Km,
                FuelType = dto.FuelType
            };
    }
}