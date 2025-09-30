using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Globalization;
using ControleGastosApp.Services;

namespace ControleGastosApp;

public partial class MainPage : ContentPage
{
    private decimal totalGasto = 0;

    public MainPage()
    {
        InitializeComponent();
        _ = CarregarGastosAsync();
    }

    // Carrega todos os gastos do SQLite
    private async Task CarregarGastosAsync()
    {
        try
        {
            var gastos = await MauiProgram.GastoDb.ObterGastosAsync();
            totalGasto = gastos.Sum(g => g.Valor);
            totalLabel.Text = $"Total gasto: R$ {totalGasto:F2}";
            gastosCollectionView.ItemsSource = gastos;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro ao carregar os gastos: {ex.Message}", "OK");
        }
    }

    // Adiciona novo gasto no SQLite
    private async void OnAdicionarGastoClicked(object sender, EventArgs e)
    {
        string valorTexto = valorEntry.Text?.Trim().Replace(",", ".");

        if (decimal.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
        {
            try
            {
                var gasto = new Gasto
                {
                    Valor = valor,
                    Local = ondeEntry.Text?.Trim() ?? "",
                    FormaPagamento = formaPagamentoPicker.SelectedItem?.ToString() ?? "Não informado",
                    Data = DateTime.Now
                };

                await MauiProgram.GastoDb.SalvarGastoAsync(gasto);
                await CarregarGastosAsync();

                valorEntry.Text = string.Empty;
                ondeEntry.Text = string.Empty;
                formaPagamentoPicker.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao salvar o gasto: {ex.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Erro", "Insira um valor numérico válido!", "OK");
        }
    }

    // Testar Postgres (inserção e leitura)
    private async void OnTestarPostgresClicked(object sender, EventArgs e)
    {
        try
        {
            // 1. Buscar os gastos que já existem no SQLite
            var gastosSqlite = await MauiProgram.GastoDb.ObterGastosAsync();

            if (gastosSqlite == null || !gastosSqlite.Any())
            {
                await DisplayAlert("Postgres", "Nenhum gasto encontrado no SQLite para sincronizar.", "OK");
                return;
            }

            // 2. Inserir no Postgres
            var service = new GastoServicePg();
            foreach (var gasto in gastosSqlite)
            {
                await service.InserirGasto(gasto);
            }

            // 3. Confirmar com contagem do Postgres
            var listaPg = await service.ListarGastos();
            await DisplayAlert("Postgres", $"Sincronização concluída!\nTotal no Postgres: {listaPg.Count}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao sincronizar: {ex.Message}", "OK");
        }
    }

    // Editar gasto no SQLite
    private async void OnEditarGastoClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Gasto gasto)
        {
            string novoValor = await DisplayPromptAsync("Editar Gasto", "Novo valor:",
                                                        initialValue: gasto.Valor.ToString(CultureInfo.InvariantCulture));
            if (decimal.TryParse(novoValor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                gasto.Valor = valor;
                await MauiProgram.GastoDb.SalvarGastoAsync(gasto);
                await CarregarGastosAsync();
            }
        }
    }

    // Excluir gasto do SQLite
    private async void OnExcluirGastoClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Gasto gasto)
        {
            bool confirmar = await DisplayAlert("Confirmação",
                $"Deseja excluir o gasto de R$ {gasto.Valor:F2}?", "Sim", "Não");

            if (confirmar)
            {
                try
                {
                    await MauiProgram.GastoDb.ExcluirGastoAsync(gasto);
                    await CarregarGastosAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", $"Erro ao excluir o gasto: {ex.Message}", "OK");
                }
            }
        }
    }
}
