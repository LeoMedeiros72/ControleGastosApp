using SQLite;

namespace ControleGastosApp;

public class Gasto
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public decimal Valor { get; set; }
    public string Onde { get; set; }
    public string FormaPagamento { get; set; }
    public DateTime Data { get; set; }
}