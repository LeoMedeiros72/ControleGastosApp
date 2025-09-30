using SQLite;
using System;

namespace ControleGastosApp
{
    public class Gasto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public decimal Valor { get; set; }

        public string Onde { get; set; } = string.Empty;
        public string FormaPagamento { get; set; } = "Não informado";

        public DateTime Data { get; set; } = DateTime.Now;
    }
}
