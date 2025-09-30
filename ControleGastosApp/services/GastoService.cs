using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleGastosApp.Services
{
    public interface IGastoService
    {
        Task InserirGasto(Gasto gasto);
        Task<List<Gasto>> ListarGastos();
        Task EditarGasto(Gasto gasto);
        Task ExcluirGasto(int id);
    }
}
