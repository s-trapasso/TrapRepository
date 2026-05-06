namespace VehicleManager.App.Services;

/// <summary>
/// Wrapper per le risposte API. Contiene il dato oppure l'errore,
/// così le pagine Blazor non devono gestire eccezioni direttamente.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; private set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }

    public static ApiResponse<T> Ok(T data) => new() { Success = true, Data = data };
    public static ApiResponse<T> Fail(string error) => new() { Success = false, ErrorMessage = error };
}