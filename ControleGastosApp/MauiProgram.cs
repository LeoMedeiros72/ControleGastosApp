using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using System.IO;
using ControleGastosApp.Services;

namespace ControleGastosApp
{
    public static class MauiProgram
    {
        public static GastoDatabase GastoDb { get; private set; } = null!;
        public static IGastoService GastoService { get; private set; } = null!;

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

            // tenta Postgres, se falhar usa SQLite
            try
            {
                var testPg = new GastoServicePg();
                // tentativa rápida de conexão
                using var conn = new Npgsql.NpgsqlConnection("Host=localhost;Port=5432;Username=admin;Password=admin;Database=contabilidade");
                conn.Open();
                GastoService = new GastoServicePg();
            }
            catch
            {
                GastoService = new GastoServiceSqlite(GastoDb);
            }

            return builder.Build();
        }
    }
}
