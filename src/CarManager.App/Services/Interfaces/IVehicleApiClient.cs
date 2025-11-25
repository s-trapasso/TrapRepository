using CarManager.App.Models.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Services.Interfaces
{
    public interface IVehicleApiClient
    {
        Task<IReadOnlyList<VehicleModel>> GetAllVehiclesAsync(CancellationToken cancellationToken = default);
        Task<VehicleModel> CreateVehicleAsync(VehicleCreateModel vehicleCreateModel, CancellationToken cancellationToken = default);
    }
}
