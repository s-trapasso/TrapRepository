using GestioneVeicoli.Views;

namespace GestioneVeicoli
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("veicoloDettaglio", typeof(VeicoloDettaglioPage));
        }
    }
}
