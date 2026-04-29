using CarManager.App.Models.Vehicle;
using CarManager.App.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CarManager.App.Services.Implementations
{
    public class VehicleApiClient : IVehicleApiClient
    {
        private readonly HttpClient _httpClient;
        public VehicleApiClient(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public async Task<IReadOnlyList<VehicleModel>> GetAllVehiclesAsync(CancellationToken cancellationToken = default)
        {
            //Get all vehicles from the API /api/vehicles
            try
            {   
                var response = await _httpClient.GetAsync("api/vehicles", cancellationToken);
                response.EnsureSuccessStatusCode();

                var vehicles = await response.Content.ReadFromJsonAsync<List<VehicleModel>>(JsonOptions, cancellationToken)
                            ?? new List<VehicleModel>();

                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<VehicleModel> CreateVehicleAsync(VehicleCreateModel vehicleCreateModel, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/vehicles", vehicleCreateModel, JsonOptions, cancellationToken);
            response.EnsureSuccessStatusCode();

            var createdVehicle = await response.Content.ReadFromJsonAsync<VehicleModel>(JsonOptions, cancellationToken)
                                ?? throw new Exception("Failed to create vehicle");

            return createdVehicle;
        }

        public async Task DeleteVehicleAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"api/vehicles/{vehicleId}", cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        public async Task<VehicleModel> GetByIdAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/vehicles/{vehicleId}", cancellationToken);

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            // IMPORTANTISSIMO: vediamo cosa torna davvero
            Console.WriteLine($"GET /api/vehicles/{vehicleId} -> {(int)response.StatusCode}");
            Console.WriteLine(json);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<VehicleModel>(JsonOptions, cancellationToken)
                   ?? throw new Exception("Vehicle not found or invalid response");
        }

        public async Task UpdateVehicleAsync(int vehicleId, VehicleCreateModel vehicleUpdateModel, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/vehicles/{vehicleId}", vehicleUpdateModel, JsonOptions, cancellationToken);
            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            response.EnsureSuccessStatusCode();
        }
        
    }
}
