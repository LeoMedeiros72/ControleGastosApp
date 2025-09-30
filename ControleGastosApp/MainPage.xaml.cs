using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Globalization;
using ControleGastosApp.Services;
using System.Collections.Generic;

namespace ControleGastosApp
{
    public partial class MainPage : ContentPage
    {
        private decimal totalGasto = 0;

        public MainPage()
        {
            InitializeComponent();
            PreencherCategorias();
            _ = CarregarGastosAsync();
        }

        // 🔹 Preenche o Picker de Categorias
        private void PreencherCategorias()
        {
            var categorias = new List<string>
            {
                "Alimentação",
                "Transporte",
                "Educação",
                "Lazer",
                "Saúde",
                "Serviços",
                "Moradia",
                "Compras",
                "Compras Online",
                "Impostos",
                "Cartões",
                "Outros"
            };
            
            categoriaPicker.ItemsSource = categorias;
            subcategoriaPicker.ItemsSource = new List<string>(); // começa vazio
        }

        // 🔹 Preenche subcategorias conforme a categoria
        private void OnCategoriaSelecionada(object sender, EventArgs e)
        {
            if (categoriaPicker.SelectedItem is string categoria)
            {
                var subcategorias = new List<string>();

                switch (categoria)
                {
                    case "Alimentação":
                        subcategorias.Add("Supermercado Mensal");
                        subcategorias.Add("Supermercado Complemento");
                        subcategorias.Add("Restaurante");
                        subcategorias.Add("Lanche");
                        subcategorias.Add("Outras Alimentações");
                        break;
                    case "Transporte":
                        subcategorias.Add("Combustível");
                        subcategorias.Add("Uber/Taxi");
                        subcategorias.Add("Seguro");
                        subcategorias.Add("Documentação");
                        subcategorias.Add("Outros em Transporte");
                        subcategorias.Add("Manutenção");
                        break;
                    case "Educação":
                        subcategorias.Add("Mensalidade Escolar");
                        subcategorias.Add("Curso");
                        subcategorias.Add("Materiais Escolares");
                        subcategorias.Add("Creche");
                        subcategorias.Add("Faculdade");
                        subcategorias.Add("Inglês");
                        break;
                    case "Lazer":
                        subcategorias.Add("Cinema");
                        subcategorias.Add("Viagem");
                        subcategorias.Add("Show");
                        subcategorias.Add("Viagens Curtas");
                        subcategorias.Add("Bares/Boates");
                        subcategorias.Add("Outros em Lazer");
                        break;
                    case "Saúde":
                        subcategorias.Add("Farmácia");
                        subcategorias.Add("Plano de Saúde");
                        subcategorias.Add("Consulta");
                        break;
                    case "Serviços":
                        subcategorias.Add("Internet");
                        subcategorias.Add("Telefonia");
                        subcategorias.Add("Energia");
                        subcategorias.Add("Água");
                        break;
                    case "Moradia":
                        subcategorias.Add("Aluguel");
                        subcategorias.Add("Condomínio");
                        subcategorias.Add("Reforma");
                        break;
                    case "Compras":
                        subcategorias.Add("Roupas");
                        subcategorias.Add("Eletrônicos");
                        subcategorias.Add("Móveis");
                        subcategorias.Add("Eletrodomésticos");
                        subcategorias.Add("Outros em Compras");
                        break;
                    case "Compras Online":
                        subcategorias.Add("Shoppe");
                        subcategorias.Add("Amazon");
                        subcategorias.Add("Mercado Livre");
                        subcategorias.Add("AliExpress");
                        subcategorias.Add("Temu");
                        subcategorias.Add("Shein");
                        subcategorias.Add("Outros em Compras Online");
                        break;  
                    case "Impostos":
                        subcategorias.Add("IPTU");
                        subcategorias.Add("IPVA");
                        subcategorias.Add("IRPF");
                        subcategorias.Add("Multas");
                        subcategorias.Add("Taxas Bancárias");
                        break;
                    case "Cartões":
                        subcategorias.Add("Fatura do Cartão");
                        subcategorias.Add("Anuidade");
                        subcategorias.Add("Outros em Cartões");
                        break;
                    case "Outros":
                        subcategorias.Add("Outros");
                        break;
                }

                subcategoriaPicker.ItemsSource = subcategorias;
            }
        }

        private string ObterNomeCategoria(int? categoriaId)
{
    if (categoriaId == null) return "Sem categoria";

    var categorias = new List<string>
    {
        "Alimentação",
        "Transporte",
        "Educação",
        "Lazer",
        "Saúde",
        "Serviços",
        "Moradia",
        "Compras",
        "Compras Online",
        "Impostos",
        "Cartões",
        "Outros"
    };

    return (categoriaId > 0 && categoriaId <= categorias.Count) 
        ? categorias[categoriaId.Value - 1] 
        : "Desconhecida";
}

        private string ObterNomeSubcategoria(int? categoriaId, int? subcategoriaId)
        {
            if (categoriaId == null || subcategoriaId == null) return "Sem subcategoria";

            var subcategorias = new Dictionary<int, List<string>>
            {
                { 1, new List<string>{ "Supermercado Mensal", "Supermercado Complemento", "Restaurante", "Lanche", "Outras Alimentações" } },
                { 2, new List<string>{ "Combustível", "Uber/Taxi", "Seguro", "Documentação", "Outros em Transporte", "Manutenção" } },
                { 3, new List<string>{ "Mensalidade Escolar", "Curso", "Materiais Escolares", "Creche", "Faculdade", "Inglês" } },
                { 4, new List<string>{ "Cinema", "Viagem", "Show", "Viagens Curtas", "Bares/Boates", "Outros em Lazer" } },
                { 5, new List<string>{ "Farmácia", "Plano de Saúde", "Consulta" } },
                { 6, new List<string>{ "Internet", "Telefonia", "Energia", "Água" } },
                { 7, new List<string>{ "Aluguel", "Condomínio", "Reforma" } },
                { 8, new List<string>{ "Roupas", "Eletrônicos", "Móveis", "Eletrodomésticos", "Outros em Compras" } },
                { 9, new List<string>{ "Shoppe", "Amazon", "Mercado Livre", "AliExpress", "Temu", "Shein", "Outros em Compras Online" } },
                { 10, new List<string>{ "IPTU", "IPVA", "IRPF", "Multas", "Taxas Bancárias" } },
                { 11, new List<string>{ "Fatura do Cartão", "Anuidade", "Outros em Cartões" } },
                { 12, new List<string>{ "Outros" } }
            };

            if (subcategorias.ContainsKey(categoriaId.Value))
            {
                var lista = subcategorias[categoriaId.Value];
                return (subcategoriaId > 0 && subcategoriaId <= lista.Count)
                    ? lista[subcategoriaId.Value - 1]
                    : "Desconhecida";
            }

            return "Desconhecida";
        }

        // 🔹 Carrega todos os gastos do SQLite
        private async Task CarregarGastosAsync()
        {
            try
            {
                var gastos = await MauiProgram.GastoDb.ObterGastosAsync();

                foreach (var g in gastos)
                {
                    g.CategoriaNome = ObterNomeCategoria(g.CategoriaId);
                    g.SubcategoriaNome = ObterNomeSubcategoria(g.CategoriaId, g.SubcategoriaId);
                }

                totalGasto = gastos.Sum(g => g.Valor);
                totalLabel.Text = $"Total gasto: R$ {totalGasto:F2}";
                gastosCollectionView.ItemsSource = gastos;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao carregar os gastos: {ex.Message}", "OK");
            }
        }

        // 🔹 Adiciona novo gasto
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
                        Data = DateTime.Now,
                        CategoriaId = categoriaPicker.SelectedIndex >= 0 ? categoriaPicker.SelectedIndex + 1 : (int?)null,
                        SubcategoriaId = subcategoriaPicker.SelectedIndex >= 0 ? subcategoriaPicker.SelectedIndex + 1 : (int?)null
                    };

                    await MauiProgram.GastoDb.SalvarGastoAsync(gasto);
                    await CarregarGastosAsync();

                    // limpa os campos
                    valorEntry.Text = string.Empty;
                    ondeEntry.Text = string.Empty;
                    formaPagamentoPicker.SelectedIndex = -1;
                    categoriaPicker.SelectedIndex = -1;
                    subcategoriaPicker.SelectedIndex = -1;
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

        // 🔹 Migrar dados para o Postgres
        private async void OnMigrarDadosClicked(object sender, EventArgs e)
        {
            try
            {
                var gastosSqlite = await MauiProgram.GastoDb.ObterGastosAsync();

                if (gastosSqlite == null || !gastosSqlite.Any())
                {
                    await DisplayAlert("Postgres", "Nenhum gasto encontrado no SQLite para migrar.", "OK");
                    return;
                }

                var service = new GastoServicePg();
                foreach (var gasto in gastosSqlite)
                {
                    await service.InserirGasto(gasto);
                }

                var listaPg = await service.ListarGastos();
                await DisplayAlert("Postgres", $"Migração concluída!\nTotal no Postgres: {listaPg.Count}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Falha ao migrar: {ex.Message}", "OK");
            }
        }

        // 🔹 Editar gasto
        private async void OnEditarGastoClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is Gasto gasto)
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

        // 🔹 Excluir gasto
        private async void OnExcluirGastoClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is Gasto gasto)
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
}
