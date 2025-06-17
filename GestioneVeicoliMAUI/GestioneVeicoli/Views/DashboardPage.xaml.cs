using System.Collections.ObjectModel;
using System.Windows.Input;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.ViewModels;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Core.Carousel;

namespace GestioneVeicoli.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel dashboardViewModel)
    {
		InitializeComponent();
        BindingContext = dashboardViewModel;
    }
    private async void OnNavigateClicked(object sender, EventArgs e)
    {
        if (sender is SfButton button)
        {
            var route = button.ClassId;
            await Shell.Current.GoToAsync($"//{route}");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if(BindingContext is DashboardViewModel viewModel)
        {
            _= viewModel.LoadDataAsync();
        }
    }
}