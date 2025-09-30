using SQLite;
using System;

namespace ControleGastosApp
{
    public class Gasto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public decimal Valor { get; set; }

        // Onde foi gasto → no Postgres é a coluna "local"
        public string Local { get; set; } = string.Empty;

        // Forma de pagamento → no Postgres é a coluna "forma_pagamento"
        public string FormaPagamento { get; set; } = "Não informado";

        public DateTime Data { get; set; } = DateTime.Now;

        // Campos opcionais
        public int? CategoriaId { get; set; }
        public int? SubcategoriaId { get; set; }

        public string CategoriaNome { get; set; }
        public string SubcategoriaNome { get; set; }

    }
}
