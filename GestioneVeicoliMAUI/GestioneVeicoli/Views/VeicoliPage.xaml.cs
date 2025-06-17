using GestioneVeicoli.Data.Models;
using GestioneVeicoli.ViewModels;

namespace GestioneVeicoli.Views;

public partial class VeicoliPage : ContentPage
{
    private readonly VeicoloViewModel _VeicoloViewModel;
    public VeicoliPage(VeicoloViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _VeicoloViewModel = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is VeicoloViewModel viewModel)
        {
            _ = viewModel.LoadDataAsync();
        }
    }
}