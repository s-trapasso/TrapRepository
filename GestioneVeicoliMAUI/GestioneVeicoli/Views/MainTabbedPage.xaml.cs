namespace GestioneVeicoli.Views;

public partial class MainTabbedPage : TabbedPage
{
	public MainTabbedPage(VeicoliPage veicoliPage, ProprietarioPage proprietarioPage)
	{
		InitializeComponent();

		Children.Add(veicoliPage);
        Children.Add(proprietarioPage);
    }
}