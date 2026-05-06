using VehicleManager.Shared.DTOs;

namespace VehicleManager.App.Services;

// ═════════════════════════════════════════════════════════════════════════════
// MAINTENANCE SERVICE
// ═════════════════════════════════════════════════════════════════════════════

public interface IMaintenanceService
{
    Task<ApiResponse<List<MaintenanceDto>>> GetAllAsync(int? vehicleId = null);
    Task<ApiResponse<MaintenanceDto>> GetByIdAsync(int id);
    Task<ApiResponse<decimal>> GetTotaleCostiAsync(int vehicleId);
    Task<ApiResponse<MaintenanceDto>> CreateAsync(CreateMaintenanceDto dto);
    Task<ApiResponse<MaintenanceDto>> UpdateAsync(int id, UpdateMaintenanceDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}

public class MaintenanceService : BaseApiService, IMaintenanceService
{
    private const string Base = "api/maintenances";

    public MaintenanceService(HttpClient http) : base(http) { }

    public Task<ApiResponse<List<MaintenanceDto>>> GetAllAsync(int? vehicleId = null)
        => GetAsync<List<MaintenanceDto>>(vehicleId.HasValue ? $"{Base}?vehicleId={vehicleId}" : Base);

    public Task<ApiResponse<MaintenanceDto>> GetByIdAsync(int id)
        => GetAsync<MaintenanceDto>($"{Base}/{id}");

    public Task<ApiResponse<decimal>> GetTotaleCostiAsync(int vehicleId)
        => GetAsync<decimal>($"{Base}/vehicle/{vehicleId}/totale-costi");

    public Task<ApiResponse<MaintenanceDto>> CreateAsync(CreateMaintenanceDto dto)
        => PostAsync<MaintenanceDto>(Base, dto);

    public Task<ApiResponse<MaintenanceDto>> UpdateAsync(int id, UpdateMaintenanceDto dto)
        => PutAsync<MaintenanceDto>($"{Base}/{id}", dto);

    public Task<ApiResponse<bool>> DeleteAsync(int id)
        => DeleteAsync($"{Base}/{id}");
}

// ═════════════════════════════════════════════════════════════════════════════
// OWNER SERVICE
// ═════════════════════════════════════════════════════════════════════════════

public interface IOwnerService
{
    Task<ApiResponse<List<OwnerDto>>> GetAllAsync();
    Task<ApiResponse<OwnerDto>> GetByIdAsync(int id);
    Task<ApiResponse<List<OwnerDto>>> GetConPatenteInScadenzaAsync(int giorni = 30);
    Task<ApiResponse<List<OwnerDto>>> GetConVisitaInScadenzaAsync(int giorni = 30);
    Task<ApiResponse<OwnerDto>> CreateAsync(CreateOwnerDto dto);
    Task<ApiResponse<OwnerDto>> UpdateAsync(int id, UpdateOwnerDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    Task<ApiResponse<VehicleOwnershipDto>> AssegnaVeicoloAsync(CreateVehicleOwnershipDto dto);
}

public class OwnerService : BaseApiService, IOwnerService
{
    private const string Base = "api/owners";

    public OwnerService(HttpClient http) : base(http) { }

    public Task<ApiResponse<List<OwnerDto>>> GetAllAsync()
        => GetAsync<List<OwnerDto>>(Base);

    public Task<ApiResponse<OwnerDto>> GetByIdAsync(int id)
        => GetAsync<OwnerDto>($"{Base}/{id}");

    public Task<ApiResponse<List<OwnerDto>>> GetConPatenteInScadenzaAsync(int giorni = 30)
        => GetAsync<List<OwnerDto>>($"{Base}/patente-in-scadenza?giorni={giorni}");

    public Task<ApiResponse<List<OwnerDto>>> GetConVisitaInScadenzaAsync(int giorni = 30)
        => GetAsync<List<OwnerDto>>($"{Base}/visita-in-scadenza?giorni={giorni}");

    public Task<ApiResponse<OwnerDto>> CreateAsync(CreateOwnerDto dto)
        => PostAsync<OwnerDto>(Base, dto);

    public Task<ApiResponse<OwnerDto>> UpdateAsync(int id, UpdateOwnerDto dto)
        => PutAsync<OwnerDto>($"{Base}/{id}", dto);

    public Task<ApiResponse<bool>> DeleteAsync(int id)
        => DeleteAsync($"{Base}/{id}");

    public Task<ApiResponse<VehicleOwnershipDto>> AssegnaVeicoloAsync(CreateVehicleOwnershipDto dto)
        => PostAsync<VehicleOwnershipDto>($"{Base}/ownership", dto);
}

// ═════════════════════════════════════════════════════════════════════════════
// DEADLINE SERVICE
// ═════════════════════════════════════════════════════════════════════════════

public interface IDeadlineService
{
    Task<ApiResponse<List<DeadlineDto>>> GetAllAsync(int? vehicleId = null);
    Task<ApiResponse<DeadlineDto>> GetByIdAsync(int id);
    Task<ApiResponse<List<DeadlineDto>>> GetInScadenzaAsync(int giorni = 30);
    Task<ApiResponse<List<DeadlineDto>>> GetScaduteAsync();
    Task<ApiResponse<DashboardAlertData>> GetDashboardAlertAsync();
    Task<ApiResponse<DeadlineDto>> CreateAsync(CreateDeadlineDto dto);
    Task<ApiResponse<DeadlineDto>> UpdateAsync(int id, UpdateDeadlineDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}

public class DeadlineService : BaseApiService, IDeadlineService
{
    private const string Base = "api/deadlines";

    public DeadlineService(HttpClient http) : base(http) { }

    public Task<ApiResponse<List<DeadlineDto>>> GetAllAsync(int? vehicleId = null)
        => GetAsync<List<DeadlineDto>>(vehicleId.HasValue ? $"{Base}?vehicleId={vehicleId}" : Base);

    public Task<ApiResponse<DeadlineDto>> GetByIdAsync(int id)
        => GetAsync<DeadlineDto>($"{Base}/{id}");

    public Task<ApiResponse<List<DeadlineDto>>> GetInScadenzaAsync(int giorni = 30)
        => GetAsync<List<DeadlineDto>>($"{Base}/in-scadenza?giorni={giorni}");

    public Task<ApiResponse<List<DeadlineDto>>> GetScaduteAsync()
        => GetAsync<List<DeadlineDto>>($"{Base}/scadute");

    public Task<ApiResponse<DashboardAlertData>> GetDashboardAlertAsync()
        => GetAsync<DashboardAlertData>($"{Base}/dashboard-alert");

    public Task<ApiResponse<DeadlineDto>> CreateAsync(CreateDeadlineDto dto)
        => PostAsync<DeadlineDto>(Base, dto);

    public Task<ApiResponse<DeadlineDto>> UpdateAsync(int id, UpdateDeadlineDto dto)
        => PutAsync<DeadlineDto>($"{Base}/{id}", dto);

    public Task<ApiResponse<bool>> DeleteAsync(int id)
        => DeleteAsync($"{Base}/{id}");
}