using CarManager.App.Services.Implementations;
using CarManager.App.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace CarManager.App
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
            builder.Services.AddMudServices();
            builder.Logging.ClearProviders();
            builder.Logging.AddDebug();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient<IVehicleApiClient,VehicleApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7260/");
            });
            builder.Services.AddHttpClient<IOwnerApiClient, OwnerApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7260/");
            });
            builder.Services.AddHttpClient<IMaintenanceApiClient, MaintenanceApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7260/");
            });
            return builder.Build();
        }
    }
}
