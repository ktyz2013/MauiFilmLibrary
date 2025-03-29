using Microsoft.Extensions.Logging;
using MauiFilmLibrary.View;
using MauiFilmLibrary.Model;
using MauiFilmLibrary.ViewModel;
using MauiFilmLibrary.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MauiFilmLibrary
{
    public static class MauiProgram
    {
        private static object _lockDb = new();
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
#endif

            builder.Services.AddTransient<MovieDatabaseContext>((services) =>
            {
                lock (_lockDb)
                {
                    var filenameDb = Path.Combine(FileSystem.AppDataDirectory, "movie_database.db");
                    if (!File.Exists(filenameDb))
                    {
                        using var stream = FileSystem.OpenAppPackageFileAsync("Db/movie_database.db").GetAwaiter().GetResult();
                        using (var memoryStream = new MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            File.WriteAllBytes(filenameDb, memoryStream.ToArray());
                        }
                    }
                    return new MovieDatabaseContext(filenameDb);
                }
            });

            builder.Services.AddSingleton<MovieService>();
            builder.Services.AddSingleton<AppShell>();

            builder.Services.AddTransient<SearchViewModel>();
            builder.Services.AddTransient<Func<MovieDto, MovieViewModel>>(sp =>
                (dto) => new MovieViewModel(dto, sp));

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MovieView>(); 

            return builder.Build();
        }
    }
}
