using CarManager.Api.Common;
using CarManager.Api.DTOs.Owner;

namespace CarManager.Api.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<List<OwnerDTO>> GetAllAsync();
        Task<OwnerDTO?> GetByIdAsync(int id);

        Task<OwnerDTO?> GetWithVehiclesAsync(int id);

        Task<Result<OwnerDTO>> CreateAsync(CreateOwnerDTO dto);

        Task<Result<OwnerDTO>> UpdateAsync(int id, UpdateOwnerDTO dto);

        Task<Result<bool>> DeleteAsync(int id);

        Task<List<OwnerDTO>> SearchAsync(string term);
    }
}
