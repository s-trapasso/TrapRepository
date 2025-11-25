using CarManager.App.Models.Owners;
using CarManager.App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
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
    }
}
