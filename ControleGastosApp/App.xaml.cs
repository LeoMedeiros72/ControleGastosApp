namespace ControleGastosApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Garante que o estilo da NavigationPage será aplicado
        MainPage = new NavigationPage(new MainPage());
    }
}
