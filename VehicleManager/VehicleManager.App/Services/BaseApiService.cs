using System.Net.Http.Json;
using System.Text.Json;

namespace VehicleManager.App.Services;

/// <summary>
/// Classe base per tutti i servizi client.
/// Gestisce la serializzazione JSON e gli errori HTTP in modo uniforme.
/// </summary>
public abstract class BaseApiService
{
    protected readonly HttpClient _http;

    protected static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        // Gestisce i DateOnly serializzati come stringa ISO
        Converters = { new DateOnlyJsonConverter() }
    };

    protected BaseApiService(HttpClient http)
    {
        _http = http;
    }

    protected async Task<ApiResponse<T>> GetAsync<T>(string url)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<T>(url, JsonOptions);
            return result != null
                ? ApiResponse<T>.Ok(result)
                : ApiResponse<T>.Fail("Nessun dato ricevuto.");
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse<T>.Fail($"Errore di rete: {ex.Message}");
        }
        catch (Exception ex)
        {
            return ApiResponse<T>.Fail($"Errore: {ex.Message}");
        }
    }

    protected async Task<ApiResponse<T>> PostAsync<T>(string url, object body)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(url, body, JsonOptions);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<T>(JsonOptions);
                return ApiResponse<T>.Ok(result!);
            }
            var error = await response.Content.ReadAsStringAsync();
            return ApiResponse<T>.Fail($"Errore {(int)response.StatusCode}: {error}");
        }
        catch (Exception ex)
        {
            return ApiResponse<T>.Fail($"Errore: {ex.Message}");
        }
    }

    protected async Task<ApiResponse<T>> PutAsync<T>(string url, object body)
    {
        try
        {
            var response = await _http.PutAsJsonAsync(url, body, JsonOptions);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<T>(JsonOptions);
                return ApiResponse<T>.Ok(result!);
            }
            var error = await response.Content.ReadAsStringAsync();
            return ApiResponse<T>.Fail($"Errore {(int)response.StatusCode}: {error}");
        }
        catch (Exception ex)
        {
            return ApiResponse<T>.Fail($"Errore: {ex.Message}");
        }
    }

    protected async Task<ApiResponse<bool>> PatchAsync(string url, object body)
    {
        try
        {
            var response = await _http.PatchAsJsonAsync(url, body, JsonOptions);
            return response.IsSuccessStatusCode
                ? ApiResponse<bool>.Ok(true)
                : ApiResponse<bool>.Fail($"Errore {(int)response.StatusCode}");
        }
        catch (Exception ex)
        {
            return ApiResponse<bool>.Fail($"Errore: {ex.Message}");
        }
    }

    protected async Task<ApiResponse<bool>> DeleteAsync(string url)
    {
        try
        {
            var response = await _http.DeleteAsync(url);
            return response.IsSuccessStatusCode
                ? ApiResponse<bool>.Ok(true)
                : ApiResponse<bool>.Fail($"Errore {(int)response.StatusCode}");
        }
        catch (Exception ex)
        {
            return ApiResponse<bool>.Fail($"Errore: {ex.Message}");
        }
    }
}