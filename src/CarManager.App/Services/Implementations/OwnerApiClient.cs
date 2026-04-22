using CarManager.App.Models.Owner;
using CarManager.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarManager.App.Services.Implementations
{
    public class OwnerApiClient : IOwnerApiClient
    {
       private readonly HttpClient _httpClient;
        public OwnerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<OwnerModel>> GetAllOwnersAsync(CancellationToken cancellationToken = default)
        {
            //Get all owners from API /api/owners
            var response = await _httpClient.GetAsync("/api/owners", cancellationToken);
            response.EnsureSuccessStatusCode();
            var owners = await response.Content.ReadFromJsonAsync<IReadOnlyList<OwnerModel>>(cancellationToken: cancellationToken)
                 ?? new List<OwnerModel>();

            return owners ;
        }
        public async Task<OwnerModel> CreateOwnerAsync(OwnerCreateModel ownerCreateModel, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/owners", ownerCreateModel, cancellationToken);
            response.EnsureSuccessStatusCode();

            var createdOwner = await response.Content.ReadFromJsonAsync<OwnerModel>(cancellationToken: cancellationToken)
                      ?? throw new Exception("Failed to create vehicle");

            return createdOwner;
        }
        public async Task<string?> GetFiscalCodePreviewAsync(OwnerCreateModel ownerCreateModel, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/owners/fiscalcode/preview", ownerCreateModel, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            // l'endpoint restituisce una stringa semplice
            var fiscalCode = await response.Content.ReadAsStringAsync(cancellationToken);
            return fiscalCode.Trim('"'); // nel caso venga serializzato come JSON string
        }
        public async Task DeleteOwnerAsync(int ownerId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"/api/owners/{ownerId}", cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<OwnerModel> GetByIdAsync(int ownerId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"/api/owners/{ownerId}", cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            // Deserializza il JSON in OwnerModel
            var owner = JsonSerializer.Deserialize<OwnerModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return owner!;
        }

        public async Task<OwnerModel> UpdateOwnerAsync(int ownerId, OwnerCreateModel ownerUpdateModel, CancellationToken cancellationToken = default)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(ownerUpdateModel),Encoding.UTF8,"application/json");

            var response = await _httpClient.PutAsync($"/api/owners/{ownerId}", jsonContent, cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var updatedOwner = JsonSerializer.Deserialize<OwnerModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return updatedOwner!;
        }
    }
}
