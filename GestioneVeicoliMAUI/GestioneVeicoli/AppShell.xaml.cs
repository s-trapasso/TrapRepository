using GestioneVeicoli.Views;

namespace GestioneVeicoli
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute("veicoloDettaglio", typeof(VeicoloDettaglioPage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(VeicoliPage), typeof(VeicoliPage));
            Routing.RegisterRoute(nameof(ManutenzioniPage), typeof(ManutenzioniPage));
            Routing.RegisterRoute(nameof(ProprietarioPage), typeof(ProprietarioPage));
        }
    }
}
