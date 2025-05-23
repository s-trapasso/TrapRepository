using Syncfusion.Maui.Buttons;

namespace GestioneVeicoli.Views;

public partial class MainTabbedPage : ContentPage
{
	public MainTabbedPage()
	{
		InitializeComponent();

		//Children.Add(veicoliPage);
  //      Children.Add(proprietarioPage);
    }
    private async void OnNavigateClicked(object sender, EventArgs e)
    {
        if (sender is SfButton button)
        {
            var route = button.ClassId;
            await Shell.Current.GoToAsync($"//{route}");
        }
    }
}