using CarManager.App.Models.Owner;
using CarManager.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarManager.App.Services.Implementations
{
    public class OwnerApiClient : IOwnerApiClient
    {
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
        public OwnerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IReadOnlyList<OwnerModel>> GetAllOwnersAsync(CancellationToken cancellationToken = default)
        {
            //Get all owners from API /api/owners
            var response = await _httpClient.GetAsync("/api/owners", cancellationToken);
            response.EnsureSuccessStatusCode();
            var owners = await response.Content.ReadFromJsonAsync<IReadOnlyList<OwnerModel>>(JsonOptions, cancellationToken: cancellationToken)
                 ?? new List<OwnerModel>();

            return owners ;
        }
        public async Task<OwnerModel> CreateOwnerAsync(OwnerCreateModel ownerCreateModel, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/owners", ownerCreateModel, JsonOptions, cancellationToken);
            response.EnsureSuccessStatusCode();

            var createdOwner = await response.Content.ReadFromJsonAsync<OwnerModel>(JsonOptions, cancellationToken: cancellationToken)
                      ?? throw new Exception("Failed to create owner");

            return createdOwner;
        }
        public async Task<string?> GetFiscalCodePreviewAsync(OwnerCreateModel ownerCreateModel, CancellationToken cancellationToken = default)
        {
            var preview = new FiscalCodePreviewModel(
                ownerCreateModel.FirstName,
                ownerCreateModel.LastName,
                ownerCreateModel.BirthDate!.Value,
                ownerCreateModel.BirthPlace,
                ownerCreateModel.Gender);

            var response = await _httpClient.PostAsJsonAsync("api/owners/fiscalcode/preview", preview, JsonOptions, cancellationToken);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
        public async Task DeleteOwnerAsync(int ownerId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"api/owners/{ownerId}", cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<OwnerModel> GetByIdAsync(int ownerId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/owners/{ownerId}", cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OwnerModel>(JsonOptions, cancellationToken)
                   ?? throw new Exception("Owner not found or invalid response");
        }

        public async Task UpdateOwnerAsync(int ownerId, OwnerCreateModel ownerUpdateModel, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/owners/{ownerId}", ownerUpdateModel, JsonOptions, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
    }
}
