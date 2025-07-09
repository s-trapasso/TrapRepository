using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services;
using CarDesk.Data.Services.Interfaces;
using CarDesk.Data.Services.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Serilog;
using Serilog.Events;

namespace CarDesk;

public static class MauiProgram
{
    public static IConfiguration Config { get; private set; } = default!;

    public static MauiApp CreateMauiApp()
    {
        /* ---------- 1.  Configura Serilog prima di tutto ---------- */
        var logDir = Path.Combine(AppContext.BaseDirectory, "logs");
        Directory.CreateDirectory(logDir);                    // crea se non esiste
        var logPath = Path.Combine(logDir, "Cardesk-.log");   // “-” = rolling daily

        Log.Logger = new LoggerConfiguration()
                     .MinimumLevel.Debug()
                     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)   // resto di Microsoft
                     .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command",
                            LogEventLevel.Warning)                   // ⬅️ SQL solo se Warning+
#if DEBUG
     .WriteTo.Debug()
#endif
     .WriteTo.Async(a => a.File(
         logPath,
         restrictedToMinimumLevel: LogEventLevel.Information,
         rollingInterval: RollingInterval.Day,
         retainedFileCountLimit: 7))
     .CreateLogger();

        /* ---------- 2.  Crea il builder MAUI ---------- */
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        /* ---------- 3.  Logging: usa solo Serilog ---------- */
        builder.Logging.ClearProviders()       // rimuove provider predefiniti
                      .AddSerilog(Log.Logger);

        /* ---------- 4.  Configurazione (appSettings.json) ---------- */
        builder.Configuration.AddJsonFile(
            "appSettings.json",
            optional: false,
            reloadOnChange: true);

        Config = builder.Configuration;        // espone la config statica

        /* ---------- 5.  Servizi DI ---------- */
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddLocalization();
        builder.Services.AddMudServices();
        ConfiguraServizi(builder.Services);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        return builder.Build();
    }

    private static void ConfiguraServizi(IServiceCollection services)
    {
        //Registrazione LoggingService
        services.AddScoped(typeof(ILoggingService<>), typeof(LoggingService<>));

        // DbContext
        var connectionString = Config.GetConnectionString("CarDeskDBDev");
        services.AddDbContextFactory<CarDeskDbContext>(opt => opt.UseSqlServer(connectionString));

        // Repository & manager
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddHttpClient<OpenMeteoService>();
    }
}
