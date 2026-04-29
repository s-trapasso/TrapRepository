using CarManager.Api.Common;
using CarManager.Api.DTOs.Vehicle;
using CarManager.Core.Enums;

namespace CarManager.Api.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehicleDTO>> GetAllAsync();
        Task<VehicleDTO?> GetByIdAsync(int id);
        Task<Result<VehicleDTO>> CreateAsync(CreateVehicleDTO dto);
        Task<Result<VehicleDTO>> UpdateAsync(int id, UpdateVehicleDTO dto);
        Task<Result<bool>> DeleteAsync(int id);
        Task<List<VehicleDTO>> SearchByPlateAsync(string plate);

    }
}
