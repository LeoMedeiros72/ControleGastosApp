using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleGastosApp.Services
{
    public class GastoServicePg : IGastoService
    {
        private readonly string _connectionString =
            "Host=localhost;Port=5432;Username=admin;Password=admin;Database=contabilidade";

        public async Task InserirGasto(Gasto gasto)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"INSERT INTO gastos (descricao, valor, data, categoria_id, subcategoria_id)
                        VALUES (@desc, @valor, @data, @cat, @sub)";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("desc", $"{gasto.Onde} - {gasto.FormaPagamento}");
            cmd.Parameters.AddWithValue("valor", gasto.Valor);
            cmd.Parameters.AddWithValue("data", gasto.Data);
            cmd.Parameters.AddWithValue("cat", 1); // categoria padrão
            cmd.Parameters.AddWithValue("sub", 1); // subcategoria padrão

            await cmd.ExecuteNonQueryAsync();
        }


        public async Task<List<Gasto>> ListarGastos()
        {
            var lista = new List<Gasto>();

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = "SELECT id, descricao, valor, data FROM gastos ORDER BY data DESC";
            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Gasto
                {
                    Id = reader.GetInt32(0),
                    Onde = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Valor = reader.GetDecimal(2),
                    Data = reader.GetDateTime(3),
                    FormaPagamento = "Postgres"
                });
            }

            return lista;
        }

        public async Task EditarGasto(Gasto gasto)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"UPDATE gastos SET descricao=@desc, valor=@valor, data=@data WHERE id=@id";

            using var cmd = new Npgsql.NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", gasto.Id);
            cmd.Parameters.AddWithValue("desc", gasto.Onde ?? "");
            cmd.Parameters.AddWithValue("valor", gasto.Valor);
            cmd.Parameters.AddWithValue("data", gasto.Data);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ExcluirGasto(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = "DELETE FROM gastos WHERE id=@id";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
