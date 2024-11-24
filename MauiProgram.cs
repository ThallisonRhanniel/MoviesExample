using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using MoviesExample.Services;
using MoviesExample.View;
using MoviesExample.ViewModel;

namespace MoviesExample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IConnectivity>((e) => Connectivity.Current);

            builder.Services.AddSingleton<MoviesService>();


            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomePageViewModel>();

            builder.Services.AddSingleton<MovieDetails>();
            builder.Services.AddSingleton<MovieDetailsViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
