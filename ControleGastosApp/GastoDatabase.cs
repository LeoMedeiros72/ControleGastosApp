using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ControleGastosApp
{
    public class GastoDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public GastoDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Gasto>().Wait();
        }

        // Listar gastos mais recentes primeiro
        public Task<List<Gasto>> ObterGastosAsync()
        {
            return _database.Table<Gasto>()
                            .OrderByDescending(g => g.Data)
                            .ToListAsync();
        }

        // Inserir ou atualizar gasto
        public Task<int> SalvarGastoAsync(Gasto gasto)
        {
            if (gasto.Id != 0)
                return _database.UpdateAsync(gasto);
            else
                return _database.InsertAsync(gasto);
        }

        // Excluir gasto
        public Task<int> ExcluirGastoAsync(Gasto gasto)
        {
            return _database.DeleteAsync(gasto);
        }
    }
}
