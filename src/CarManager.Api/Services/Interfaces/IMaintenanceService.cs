using CarManager.Api.Common;
using CarManager.Api.DTOs;
using CarManager.Api.DTOs.Maintenance;


namespace CarManager.Api.Services.Interfaces
{
    public interface IMaintenanceService
    {
        Task<List<MaintenanceDTO>> GetAllAsync();
        Task<List<MaintenanceDTO>> GetByVehicleIdAsync(int vehicleId);
        Task<MaintenanceDTO?> GetByIdAsync(int id);

        Task<Result<MaintenanceDTO>> CreateAsync(CreateMaintenanceDTO dto);

        Task<Result<MaintenanceDTO>> UpdateAsync(int id, UpdateMaintenanceDTO dto);

        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<bool>> RegisterTireChangeAsync(TireChangeDTO dto);
    }
}
