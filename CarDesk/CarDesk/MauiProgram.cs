using CarDesk.Data.Data;
using CarDesk.Data.Services.Interfaces;
using CarDesk.Data.Services.Repository;
using CarDesk.Data.Services.VeicoloRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarDesk
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
            builder.Configuration.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            var connectionString = builder.Configuration.GetConnectionString("CarDeskDBDev");
            builder.Services.AddDbContext<CarDeskDbContext>(options => options.UseSqlServer(connectionString));

            // Repository
            builder.Services.AddScoped<IVeicoliRepository, VeicoliRepository>();
            builder.Services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
            builder.Services.AddScoped<IManutenzioneRepository, ManutenzioneRepository>();
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
