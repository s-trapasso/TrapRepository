using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace VehicleManager.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            // ── MudBlazor ────────────────────────────────────────────────────────
            builder.Services.AddMudServices();

            // ── HttpClient verso la API locale ───────────────────────────────────
            // Modifica la BaseAddress con l'IP/porta della tua macchina in sviluppo
            //builder.Services.AddHttpClient("VehicleManagerApi", client =>
            //{
            //    client.BaseAddress = new Uri("http://localhost:5000/");
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //});

            //// Registra HttpClient come default per i servizi Blazor
            //builder.Services.AddScoped(sp =>
            //    sp.GetRequiredService<IHttpClientFactory>().CreateClient("VehicleManagerApi"));

            // TODO Step 6: aggiungere i servizi client (VehicleClientService, ecc.)
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		//builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
