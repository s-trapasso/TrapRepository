using CarManager.App.Models.Maintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Services.Interfaces
{
    public interface IMaintenanceApiClient
    {
        Task<IReadOnlyList<MaintenanceModel>> GetAllMaintenancesAsync(CancellationToken cancellationToken = default);
        Task<MaintenanceModel> CreateMaintenanceAsync(MaintenanceCreateModel maintenanceCreateModel, CancellationToken cancellationToken = default);
        Task DeleteMaintenanceAsync(int maintenanceId, CancellationToken cancellationToken = default);
    }
}
