using CarManager.Api.DTOs.VehicleDTO;
using CarManager.Core.Enums;

namespace CarManager.Api.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehicleDTO>> GetAllAsync();
        Task<VehicleDTO?> GetByIdAsync(int id);
        Task<(bool Success, VehicleError Error, VehicleDTO? Vehicle)> CreateAsync(CreateVehicleDTO dto);
        Task<(bool Success, VehicleError Error)> UpdateAsync(int id, UpdateVehicleDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<VehicleDTO>> SearchByPlateAsync(string plate);

    }
}
