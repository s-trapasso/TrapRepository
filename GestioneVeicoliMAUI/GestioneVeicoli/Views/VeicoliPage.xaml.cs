using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services;
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
    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ////Questo codice fa in modo che alla selezione di un veicolo, venga aperta la pagina di dettaglio
        ////if (e.CurrentSelection.FirstOrDefault() is Veicolo veicoloSelezionato)
        ////{
        ////    // Naviga alla pagina di dettaglio con il repository
        ////    var veicoloViewModel = (VeicoloViewModel)BindingContext;
        ////    var repository = ((VeicoloViewModel)BindingContext)._veicoloRepository;
        ////    await Navigation.PushAsync(new VeicoloDettaglioPage
        ////    {
        ////        BindingContext = new VeicoloDettaglioViewModel(veicoloSelezionato, repository, veicoloViewModel)
        ////    });

        ////    // Deseleziona l'elemento per evitare selezioni persistenti
        ////    ((CollectionView)sender).SelectedItem = null;
        ////}

        //if (e.CurrentSelection.FirstOrDefault() is not Veicolo veicoloSelezionato)
        //    return;
        //try
        //{
            
        //        string azione = await DisplayActionSheet(
        //            "Scegli un'azione",
        //            "Annulla",
        //            null,
        //            "Modifica",
        //            "Elimina"
        //        );

        //       var vm = BindingContext as VeicoloViewModel;

        //    switch (azione)
        //    {
        //        case "Modifica":
        //            if (vm.SelezionaCommand.CanExecute(veicoloSelezionato))
        //                vm.SelezionaCommand.Execute(veicoloSelezionato);
        //            break;
        //        case "Elimina":
        //            if(vm.EliminaCommand.CanExecute(veicoloSelezionato))
        //            {
        //                await vm.EliminaCommand.ExecuteAsync(veicoloSelezionato);
        //            }
        //            break;
        //    }
            
        //}
        //catch (Exception ex)
        //{
        //    await DisplayAlert("Errore", $"Si è verificato un errore: {ex.Message}", "OK");
        //}
        //finally
        //{
        //    // Deseleziona l'elemento in ogni caso
        //    ((CollectionView)sender).SelectedItem = null;
        //}
    }
    // Metodo per gestire la modifica di un veicolo
    private void ModificaVeicolo(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Veicolo veicolo)
        {
            var vm = BindingContext as VeicoloViewModel;
            if (vm?.SelezionaCommand?.CanExecute(veicolo) ?? false)
            {
                vm.SelezionaCommand.Execute(veicolo);
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