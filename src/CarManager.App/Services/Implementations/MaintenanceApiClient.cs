using CarManager.App.Models.Maintenance;
using CarManager.App.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CarManager.App.Services.Implementations
{
    public class MaintenanceApiClient : IMaintenanceApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MaintenanceApiClient> _logger;
        public MaintenanceApiClient(HttpClient httpClient, ILogger<MaintenanceApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() }
        };
        public async Task<IReadOnlyList<MaintenanceModel>> GetAllMaintenancesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Richiesta GET manutenzioni");
            try
            {
                //Get all Maintenance from API /api/maintenance
                var response = await _httpClient.GetAsync("/api/maintenances", cancellationToken);
                response.EnsureSuccessStatusCode();
                var maintenances = await response.Content.ReadFromJsonAsync<IReadOnlyList<MaintenanceModel>>(JsonOptions, cancellationToken: cancellationToken)
                     ?? new List<MaintenanceModel>();

                return maintenances;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la richiesta GET manutenzioni");
                throw;
            }

        }
        public async Task<MaintenanceModel> CreateMaintenanceAsync(MaintenanceCreateModel maintenanceCreateModel, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Richiesta POST manutenzioni");
            try
            {
                //Create Maintenance to API /api/maintenance
                var response = await _httpClient.PostAsJsonAsync("api/maintenances", maintenanceCreateModel, cancellationToken);
                response.EnsureSuccessStatusCode();

                var createdMaintenances = await response.Content.ReadFromJsonAsync<MaintenanceModel>(JsonOptions, cancellationToken: cancellationToken)
                          ?? throw new Exception("Failed to create maintenances");

                return createdMaintenances;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la richiesta POST manutenzioni");
                throw;
            }

        }
        public async Task DeleteMaintenanceAsync(int maintenanceId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Richiesta DELETE manutenzioni");
            try
            {
                //Delete Maintenance to API /api/maintenance/{id}
                var response = await _httpClient.DeleteAsync($"api/maintenances/{maintenanceId}", cancellationToken);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la richiesta DELETE manutenzioni");
                throw;
            }
        }
    }
}
