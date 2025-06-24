using CarDesk.Data.Data;
using CarDesk.Data.Services;
using CarDesk.Data.Services.Interfaces;
using CarDesk.Data.Services.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarDesk
{
    public static class MauiProgram
    {
        public static IConfiguration config { get; private set; }
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
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Usa la cartella dell'eseguibile
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            config = configBuilder.Build();
            ConfigurazioneServizi(builder.Services);

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void ConfigurazioneServizi(IServiceCollection services)
        {
            // Configurazione del contesto del database
            var connectionString = config.GetConnectionString("CarDeskDBDev");
            services.AddDbContext<CarDeskDbContext>(options => options.UseSqlServer(connectionString));
            
            // Configurazione dei servizi aggiuntivi se necessario
            services.AddScoped<IVeicoliRepository, VeicoliRepository>();
            services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
            services.AddScoped<IManutenzioneRepository, ManutenzioneRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
