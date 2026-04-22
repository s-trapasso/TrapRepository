using CarManager.Api.DTOs.OwnerDTO;

namespace CarManager.Api.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<List<OwnerDTO>> GetAllAsync();
        Task<OwnerDTO?> GetByIdAsync(int id);

        Task<OwnerDTO?> GetWithVehiclesAsync(int id);

        Task<(bool Success, string? Error, OwnerDTO? Data)> CreateAsync(CreateOwnerDTO dto);

        Task<(bool Success, string? Error)> UpdateAsync(int id, UpdateOwnerDTO dto);

        Task<bool> DeleteAsync(int id);
    }
}
