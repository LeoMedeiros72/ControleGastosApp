using SQLite;

namespace ControleGastosApp;

public class GastoDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public GastoDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Gasto>().Wait();
    }

    public Task<List<Gasto>> ObterGastosAsync()
    {
        return _database.Table<Gasto>().ToListAsync();
    }

    public Task<int> SalvarGastoAsync(Gasto gasto)
    {
        return _database.InsertAsync(gasto);
    }

    public Task<int> ExcluirGastoAsync(Gasto gasto)
    {
        return _database.DeleteAsync(gasto);
    }
}
