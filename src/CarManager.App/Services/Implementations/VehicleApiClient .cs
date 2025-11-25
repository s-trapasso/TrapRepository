using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using CarManager.App.Models.Vehicle;
using CarManager.App.Services.Interfaces;

namespace CarManager.App.Services.Implementations
{
    public class VehicleApiClient : IVehicleApiClient
    {
        private readonly HttpClient _httpClient;
        public VehicleApiClient(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<IReadOnlyList<VehicleModel>> GetAllVehiclesAsync(CancellationToken cancellationToken = default)
        {
            //Get all vehicles from the API /api/vehicles
            var response = await _httpClient.GetAsync("api/vehicles", cancellationToken);
            response.EnsureSuccessStatusCode();
            
            var vehicles = await response.Content.ReadFromJsonAsync<List<VehicleModel>>(cancellationToken: cancellationToken)
                      ?? new List<VehicleModel>();

            return vehicles;
        }

        public async Task<VehicleModel> CreateVehicleAsync(VehicleCreateModel vehicleCreateModel, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/vehicles", vehicleCreateModel, cancellationToken);
            response.EnsureSuccessStatusCode();

            var createdVehicle = await response.Content.ReadFromJsonAsync<VehicleModel>(cancellationToken: cancellationToken)
                      ?? throw new Exception("Failed to create vehicle");

            return createdVehicle;
        }

    }
}
