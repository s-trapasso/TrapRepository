using GestioneVeicoli.ViewModels;

namespace GestioneVeicoli.Views;

public partial class ManutenzioniPage : ContentPage
{
    private readonly ManutenzioneViewModel _manutenzioniViewModel;
    public ManutenzioniPage(ManutenzioneViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _manutenzioniViewModel = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ManutenzioneViewModel viewModel)
        {
            _ = viewModel.LoadDataAsync();
        }
    }
}