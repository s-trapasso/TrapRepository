
using System.Reflection;
using GestioneVeicoli.Data.Data;
using GestioneVeicoli.Data.Services.Interfaces;
using GestioneVeicoli.Data.Services.Repository;
using GestioneVeicoli.Data.Services.VeicoloRepository;
using GestioneVeicoli.Log;
using GestioneVeicoli.ViewModels;
using GestioneVeicoli.Views;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace GestioneVeicoli
{
    public static class MauiProgram
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration config { get; private set; } // Aggiunto per la configurazione
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            //Lettura AppSetting.json
            var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // Usa la cartella dell'eseguibile
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
            config = configBuilder.Build();

            // Percorso del file di configurazione
            var logConfigPath = Path.Combine(AppContext.BaseDirectory, "log4net.config");

            // Crea la cartella Log se non esiste
            var logDir = Path.Combine(AppContext.BaseDirectory, "Log");
            Directory.CreateDirectory(logDir);

            // Inizializza log4net
            var repository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(repository, new FileInfo(logConfigPath));

            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ✅ Configura servizi
            ConfiguraServizi(builder.Services);

            //// ✅ Crea un ServiceProvider temporaneo per il logging
            //var tempProvider = builder.Services.BuildServiceProvider();
            //var loggerFactory = tempProvider.GetRequiredService<ILoggingServiceFactory>();
            //var logger = loggerFactory.CreateLogger(typeof(MauiProgram));
            //logger.Info("Avvio configurazione dell'applicazione...");



#if DEBUG            
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            ServiceProvider = app.Services; // Salvo il provider di servizi
            return app;
        }
        private static void ConfiguraServizi(IServiceCollection services)
        {
            // Registrazione del servizio di navigazione e log
            services.AddSingleton<NavigationService>();
            services.AddSingleton<ILoggingServiceFactory, LoggingServiceFactory>();

            // ✅ Crea un ServiceProvider temporaneo per il logging
            var tempProvider = services.BuildServiceProvider();
            var loggerFactory = tempProvider.GetRequiredService<ILoggingServiceFactory>();
            var logger = loggerFactory.CreateLogger(typeof(MauiProgram));
            logger.Info("Avvio configurazione dell'applicazione...");
            
            // Registra la configurazione nel container DI
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<VeicoliDbContext>(options => options.UseSqlServer(connectionString));

            //Registrazione Licenza Syncfusion
            var keyLicense = config["SyncfusionLicenseKey:LicenseKey"];
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(keyLicense);

            // Repository
            services.AddScoped<IVeicoliRepository, VeicoliRepository>();
            services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
            services.AddScoped<IManutenzioneRepository, ManutenzioneRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            // ViewModels
            services.AddScoped<DashboardViewModel>();
            services.AddScoped<VeicoloViewModel>();
            services.AddScoped<ProprietarioViewModel>();
            services.AddScoped<ManutenzioneViewModel>();

            //Pagine
            services.AddTransient<VeicoliPage>();

        }
    }
}