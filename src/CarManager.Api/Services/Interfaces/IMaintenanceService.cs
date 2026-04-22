using CarManager.Api.DTOs.MaintenanceDTO;


namespace CarManager.Api.Services.Interfaces
{
    public interface IMaintenanceService
    {
        Task<List<MaintenanceDTO>> GetAllAsync();
        Task<List<MaintenanceDTO>> GetByVehicleIdAsync(int vehicleId);
        Task<MaintenanceDTO?> GetByIdAsync(int id);

        Task<(bool Success, string? Error, MaintenanceDTO? Data)> CreateAsync(CreateMaintenanceDTO dto);

        Task<(bool Success, string? Error)> UpdateAsync(int id, UpdateMaintenanceDTO dto);

        Task<bool> DeleteAsync(int id);
    }
}
