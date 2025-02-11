using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace SkillPicker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            Microsoft.Maui.Handlers.ToolbarHandler.Mapper.AppendToMapping("CustomNavigationView", (handler, view) =>
            {
#if ANDROID
                Google.Android.Material.AppBar.MaterialToolbar materialToolbar = handler.PlatformView;

                materialToolbar.TitleCentered = true;
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
