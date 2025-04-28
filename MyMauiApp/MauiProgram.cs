using Microsoft.EntityFrameworkCore;
using MyMauiApp;
using MyMauiApp.Data;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "XamarinDb.db");
       /* if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
        } */
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddDbContext<AppDbContext>();

        return builder.Build();
    }
}
