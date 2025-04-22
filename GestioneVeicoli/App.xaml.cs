using GestioneVeicoli.Views;

namespace GestioneVeicoli
{
    public partial class App : Application
    {
        public App(VeicoliPage veicoliPage)
        {
            InitializeComponent();
            MainPage = new NavigationPage(veicoliPage);
        }
    }
}