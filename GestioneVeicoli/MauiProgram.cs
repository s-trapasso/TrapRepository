
using GestioneVeicoli.Services;
using GestioneVeicoli.ViewModels;
using GestioneVeicoli.Views;
using Microsoft.Extensions.Logging;

namespace GestioneVeicoli
{
    public static class MauiProgram
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton<IVeicoliRepository, VeicoliRepository>();
            builder.Services.AddSingleton<VeicoliViewModel>();
            builder.Services.AddTransient<VeicoloDettaglioViewModel>();
            builder.Services.AddTransient<VeicoliPage>();
            
#endif

            var app = builder.Build();
            ServiceProvider = app.Services; // Salvo il provider di servizi
            
            
            return app;
        }
    }
}
