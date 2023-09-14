using Camera.MAUI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace strikeneck
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseMauiCameraView().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
            /*  builder
                  .UseMauiApp<App>()
                  .UseMauiCameraView()
                  .ConfigureFonts(fonts =>
                  {
                      fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                      fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                  });*/

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}