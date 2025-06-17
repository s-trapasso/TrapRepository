using GestioneVeicoli.ViewModels;

namespace GestioneVeicoli.Views;

public partial class ProprietarioPage : ContentPage
{
    private readonly ProprietarioViewModel _ProprietarioViewModel;
    public ProprietarioPage(ProprietarioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _ProprietarioViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProprietarioViewModel viewModel)
        {
            _ = viewModel.LoadDataAsync();
        }
    }
}