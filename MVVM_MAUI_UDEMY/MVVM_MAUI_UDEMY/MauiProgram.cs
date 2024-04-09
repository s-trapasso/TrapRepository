using Microsoft.Extensions.Logging;
using MVVM_MAUI_UDEMY.Services;
using MVVM_MAUI_UDEMY.View;
using MVVM_MAUI_UDEMY.ViewModel;

namespace MVVM_MAUI_UDEMY
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton<MockNewsService>();
            builder.Services.AddSingleton<NewsViewModel>();
            builder.Services.AddSingleton<NewsPage>();
            builder.Services.AddSingleton<NewsDetailViewModel>();
            builder.Services.AddSingleton<NewsDetailPage>();
#endif

            return builder.Build();
        }
    }
}
