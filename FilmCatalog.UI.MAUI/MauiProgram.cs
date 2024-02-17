using CommunityToolkit.Maui;
using FilmCatalog.UI.MAUI.PageModels;
using FilmCatalog.UI.MAUI.Pages;
using FilmCatalog.UI.MAUI.Services;
using Microsoft.Extensions.Logging;

namespace FilmCatalog.UI.MAUI
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
                })
                .Services
                    .AddSingleton<IHttpService, HttpService>()
                    .AddSingleton<AppShell>()
                    .AddTransient<FilmsPageModel>()
                    .AddTransient<FilmsPage>()
                    .AddTransient<FilmDetailsPageModel>()
                    .AddTransient<ActorsPageModel>()
                    .AddTransient<ActorsPage>()
                    .AddTransient<CategoriesPageModel>()
                    .AddTransient<CategoriesPage>()
                    .AddTransient<DirectorsPageModel>()
                    .AddTransient<DirectorsPage>()
                    .AddTransient<FormatsPageModel>()
                    .AddTransient<FormatsPage>()
                    ;

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
