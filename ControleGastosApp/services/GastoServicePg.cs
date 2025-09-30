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

        // 🔹 Verifica se o gasto já existe antes de inserir
        private async Task<bool> ExisteGastoAsync(Gasto gasto)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"SELECT COUNT(*) 
                        FROM gastos 
                        WHERE sqlite_id = @sqliteId";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("sqliteId", gasto.Id);

            var count = (long)await cmd.ExecuteScalarAsync();
            return count > 0;
        }

        public async Task InserirGasto(Gasto gasto)
        {
            if (await ExisteGastoAsync(gasto))
            {
                Console.WriteLine("⛔ Gasto duplicado detectado. Não será inserido no Postgres.");
                return;
            }

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            // 🔹 Descobrir categoria/subcategoria
            var (cat, sub) = await ObterMapeamentoPersonalizadoAsync(gasto.Local);

            if (cat == 0 && sub == 0)
            {
                (cat, sub) = ClassificarAutomatico(gasto.Local);
            }

            gasto.CategoriaId = cat;
            gasto.SubcategoriaId = sub;

            var sql = @"INSERT INTO gastos 
                        (valor, local, forma_pagamento, data, categoria_id, subcategoria_id, sqlite_id)
                        VALUES (@valor, @local, @forma, @data, @cat, @sub, @sqliteId)";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("valor", gasto.Valor);
            cmd.Parameters.AddWithValue("local", gasto.Local);
            cmd.Parameters.AddWithValue("forma", gasto.FormaPagamento);
            cmd.Parameters.AddWithValue("data", gasto.Data);
            cmd.Parameters.AddWithValue("cat", gasto.CategoriaId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("sub", gasto.SubcategoriaId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("sqliteId", gasto.Id);

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // UNIQUE violation
            {
                Console.WriteLine("⚠️ Tentativa de duplicar gasto bloqueada pelo banco.");
            }
        }

        // ==========================================================
        // 🔹 Classificação semiautomática
        // ==========================================================

        // Fallback automático (palavras-chave)
        private (int categoriaId, int subcategoriaId) ClassificarAutomatico(string local)
        {
            if (string.IsNullOrWhiteSpace(local))
                return (15, 1); // Outros > Outros

            local = local.ToLower();

            if (local.Contains("mercado"))
                return (1, 1); // Alimentação > Compra Mensal

            if (local.Contains("restaurante") || local.Contains("lanche"))
                return (1, 3); // Alimentação > Restaurantes

            if (local.Contains("salão") || local.Contains("beleza"))
                return (13, 1); // Beleza > Salão de Beleza

            if (local.Contains("uber") || local.Contains("taxi"))
                return (14, 1); // Transporte > Uber/Taxi

            if (local.Contains("combustivel") || local.Contains("posto"))
                return (8, 1); // Veículos > Combustível

            if (local.Contains("escola") || local.Contains("faculdade") || local.Contains("curso"))
                return (10, 1); // Educação > Mensalidade Escolar

            return (15, 1); // Outros > Outros
        }

        // Busca personalizada no banco
        private async Task<(int categoriaId, int subcategoriaId)> ObterMapeamentoPersonalizadoAsync(string local)
        {
            if (string.IsNullOrWhiteSpace(local))
                return (0, 0);

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"SELECT categoria_id, subcategoria_id 
                        FROM mapeamento_gastos 
                        WHERE @local ILIKE '%' || palavra_chave || '%'";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("local", local);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return (reader.GetInt32(0), reader.GetInt32(1));
            }

            return (0, 0); // não encontrado
        }

        // ==========================================================
        // 🔹 CRUD básico
        // ==========================================================

        public async Task<List<Gasto>> ListarGastos()
        {
            var lista = new List<Gasto>();

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"SELECT id, valor, local, forma_pagamento, data, categoria_id, subcategoria_id 
                        FROM gastos 
                        ORDER BY data DESC";

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Gasto
                {
                    Id = reader.GetInt32(0),
                    Valor = reader.GetDecimal(1),
                    Local = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    FormaPagamento = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Data = reader.GetDateTime(4),
                    CategoriaId = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                    SubcategoriaId = reader.IsDBNull(6) ? null : reader.GetInt32(6)
                });
            }

            return lista;
        }

        public async Task EditarGasto(Gasto gasto)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var sql = @"UPDATE gastos 
                        SET valor=@valor, local=@local, forma_pagamento=@forma, data=@data,
                            categoria_id=@cat, subcategoria_id=@sub
                        WHERE id=@id";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", gasto.Id);
            cmd.Parameters.AddWithValue("valor", gasto.Valor);
            cmd.Parameters.AddWithValue("local", gasto.Local ?? "");
            cmd.Parameters.AddWithValue("forma", gasto.FormaPagamento ?? "");
            cmd.Parameters.AddWithValue("data", gasto.Data);
            cmd.Parameters.AddWithValue("cat", gasto.CategoriaId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("sub", gasto.SubcategoriaId ?? (object)DBNull.Value);

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
