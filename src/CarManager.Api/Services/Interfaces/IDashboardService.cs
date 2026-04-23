using CarManager.Api.DTOs;

namespace CarManager.Api.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardAsync(DashboardQuery query);
    }
}
