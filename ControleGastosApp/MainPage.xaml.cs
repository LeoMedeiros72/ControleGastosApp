using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace ControleGastosApp;

public partial class MainPage : ContentPage
{
    private decimal totalGasto = 0;

    public MainPage()
    {
        InitializeComponent();
        _ = CarregarGastosAsync();
    }

    // Carrega todos os gastos do banco de dados
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

    // Adiciona novo gasto
    private async void OnAdicionarGastoClicked(object sender, EventArgs e)
    {
        // Suporta vírgula ou ponto como separador decimal
        string valorTexto = valorEntry.Text?.Trim().Replace(",", ".");

        if (decimal.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
        {
            try
            {
                var gasto = new Gasto
                {
                    Valor = valor,
                    Onde = ondeEntry.Text?.Trim() ?? "",
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

    // Exclui um gasto selecionado
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
