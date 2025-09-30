using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleGastosApp.Services
{
    public class GastoServiceSqlite : IGastoService
    {
        private readonly GastoDatabase _db;

        public GastoServiceSqlite(GastoDatabase db)
        {
            _db = db;
        }

        public async Task InserirGasto(Gasto gasto)
        {
            await _db.SalvarGastoAsync(gasto);
        }

        public async Task<List<Gasto>> ListarGastos()
        {
            return await _db.ObterGastosAsync();
        }

        public async Task EditarGasto(Gasto gasto)
        {
            await _db.SalvarGastoAsync(gasto);
        }

        public async Task ExcluirGasto(int id)
        {
            var todos = await _db.ObterGastosAsync();
            var gasto = todos.Find(g => g.Id == id);
            if (gasto != null)
                await _db.ExcluirGastoAsync(gasto);
        }
    }
}
