using GestioneVeicoli.Views;

namespace GestioneVeicoli
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}