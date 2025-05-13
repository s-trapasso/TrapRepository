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

    // Metodo per gestire la modifica di un proprietario
    //private void ModificaProprietario(object sender, EventArgs e)
    //{
    //    if (sender is Button button && button.CommandParameter is Proprietario proprietario)
    //    {
    //        var vm = BindingContext as ProprietarioViewModel;
    //        if (vm?.SelezionaCommand?.CanExecute(proprietario) ?? false)
    //        {
    //            vm.SelezionaCommand.Execute(proprietario);
    //        }
    //    }
    //}

    // Metodo per gestire l'eliminazione di un proprietario
    //private async void EliminaProprietario(object sender, EventArgs e)
    //{
    //    if (sender is Button button && button.CommandParameter is Veicolo proprietario)
    //    {
    //        var vm = BindingContext as ProprietarioViewModel;
    //        if (vm?.EliminaCommand?.CanExecute(proprietario) ?? false)
    //        {
    //            bool confermaEliminazione = await DisplayAlert(
    //                "Conferma Eliminazione",
    //                "Sei sicuro di voler eliminare questo proprietario?",
    //                "Sì",
    //                "No");

    //            if (confermaEliminazione)
    //            {
    //                await vm.EliminaCommand.ExecuteAsync(proprietario);
    //            }
    //        }
    //    }
    //}
}