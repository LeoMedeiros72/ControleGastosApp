using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using System.IO;

namespace ControleGastosApp;

public static class MauiProgram
{
    public static GastoDatabase GastoDb { get; private set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "gastos.db3");
        GastoDb = new GastoDatabase(dbPath);

        return builder.Build();
    }
}
