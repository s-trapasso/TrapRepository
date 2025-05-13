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
    // Metodo per gestire la modifica di un veicolo
    private void ModificaVeicolo(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Veicolo veicolo)
        {
            var vm = BindingContext as VeicoloViewModel;
            if (vm?.SelezionaVeicoloCommand?.CanExecute(veicolo) ?? false)
            {
                vm.SelezionaVeicoloCommand.Execute(veicolo);
            }
        }
    }

    // Metodo per gestire l'eliminazione di un veicolo
    private async void EliminaVeicolo(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Veicolo veicolo)
        {
            var vm = BindingContext as VeicoloViewModel;
            if (vm?.EliminaCommand?.CanExecute(veicolo) ?? false)
            {
                bool confermaEliminazione = await DisplayAlert(
                    "Conferma Eliminazione",
                    "Sei sicuro di voler eliminare questo veicolo?",
                    "Sì",
                    "No");

                if (confermaEliminazione)
                {
                    await vm.EliminaCommand.ExecuteAsync(veicolo);
                }
            }
        }
    }
}