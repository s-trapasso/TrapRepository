using VehicleManager.Shared.DTOs;

namespace VehicleManager.App.Services;

public interface IVehicleService
{
    Task<ApiResponse<List<VehicleSummaryDto>>> GetAllAsync(bool soloAttivi = false);
    Task<ApiResponse<VehicleDto>> GetByIdAsync(int id);
    Task<ApiResponse<VehicleDto>> GetByTargaAsync(string targa);
    Task<ApiResponse<VehicleDto>> CreateAsync(CreateVehicleDto dto);
    Task<ApiResponse<VehicleDto>> UpdateAsync(int id, UpdateVehicleDto dto);
    Task<ApiResponse<bool>> AggiornaKmAsync(int id, int nuoviKm);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    Task<ApiResponse<List<MaintenanceDto>>> GetManutenzioniAsync(int vehicleId);
    Task<ApiResponse<List<DeadlineDto>>> GetScadenzeAsync(int vehicleId);
}

public class VehicleService : BaseApiService, IVehicleService
{
    private const string Base = "api/vehicles";

    public VehicleService(HttpClient http) : base(http) { }

    public Task<ApiResponse<List<VehicleSummaryDto>>> GetAllAsync(bool soloAttivi = false)
        => GetAsync<List<VehicleSummaryDto>>($"{Base}?soloAttivi={soloAttivi}");

    public Task<ApiResponse<VehicleDto>> GetByIdAsync(int id)
        => GetAsync<VehicleDto>($"{Base}/{id}");

    public Task<ApiResponse<VehicleDto>> GetByTargaAsync(string targa)
        => GetAsync<VehicleDto>($"{Base}/targa/{targa}");

    public Task<ApiResponse<VehicleDto>> CreateAsync(CreateVehicleDto dto)
        => PostAsync<VehicleDto>(Base, dto);

    public Task<ApiResponse<VehicleDto>> UpdateAsync(int id, UpdateVehicleDto dto)
        => PutAsync<VehicleDto>($"{Base}/{id}", dto);

    public Task<ApiResponse<bool>> AggiornaKmAsync(int id, int nuoviKm)
        => PatchAsync($"{Base}/{id}/km", nuoviKm);

    public Task<ApiResponse<bool>> DeleteAsync(int id)
        => DeleteAsync($"{Base}/{id}");

    public Task<ApiResponse<List<MaintenanceDto>>> GetManutenzioniAsync(int vehicleId)
        => GetAsync<List<MaintenanceDto>>($"{Base}/{vehicleId}/manutenzioni");

    public Task<ApiResponse<List<DeadlineDto>>> GetScadenzeAsync(int vehicleId)
        => GetAsync<List<DeadlineDto>>($"{Base}/{vehicleId}/scadenze");
}