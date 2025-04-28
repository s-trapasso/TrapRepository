
using GestioneVeicoli.Data;
using GestioneVeicoli.Services;
using GestioneVeicoli.ViewModels;
using GestioneVeicoli.Views;
using Microsoft.EntityFrameworkCore;
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

            // Configurazione del database
            builder.Services.AddDbContext<VeicoliDbContext>(options =>
                options.UseSqlServer("Server=DESKTOP-6DONDJT\\MSSQLSERVER_TRAP;Database=GestioneVeicoli;User Id=sa;Password=admintrap;Encrypt=False;"));
#if DEBUG
            builder.Logging.AddDebug();
            ConfiguraServizi(builder.Services);

#endif

            var app = builder.Build();
            //// Creazione automatica del database
            //using (var scope = app.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<VeicoliDbContext>();
            //    dbContext.Database.EnsureCreated();
            //}
            ServiceProvider = app.Services; // Salvo il provider di servizi
            
            
            return app;
        }
        private static void ConfiguraServizi(IServiceCollection services)
        {
            services.AddSingleton<IVeicoliRepository, VeicoliRepository>();
            services.AddSingleton<VeicoloViewModel>();
            services.AddTransient<VeicoloDettaglioViewModel>();
            services.AddTransient<VeicoliPage>();
            services.AddSingleton<NavigationService>(); // Registrazione del servizio di navigazione
        }
    }
}
