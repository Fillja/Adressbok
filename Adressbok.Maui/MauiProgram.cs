using Adressbok.Maui.Viewmodels;
using Microsoft.Extensions.Logging;
using Adressbok.ClassLibrary.Interfaces;
using Adressbok.ClassLibrary.Services;
using Adressbok.Maui.Views;

namespace Adressbok.Maui
{
    public static class MauiProgram
    {
        /// <summary>
        /// Dependency Injection för alla mina models och services/interfaces som finns i ClassLibrary.
        /// </summary>
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

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<EditViewModel>();
            builder.Services.AddSingleton<EditPage>();

            builder.Services.AddSingleton<AddViewModel>();
            builder.Services.AddSingleton<AddPage>();

            builder.Services.AddSingleton<IListService, ListService>();
            builder.Services.AddSingleton<IFileService, FileService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
