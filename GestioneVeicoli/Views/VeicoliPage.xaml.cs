using GestioneVeicoli.Models;
using GestioneVeicoli.ViewModels;

namespace GestioneVeicoli.Views;

public partial class VeicoliPage : ContentPage
{
    private readonly VeicoliViewModel _veicoliViewModel;
    public VeicoliPage(VeicoliViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _veicoliViewModel = viewModel;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //Questo codice fa in modo che alla selezione di un veicolo, venga aperta la pagina di dettaglio
        //if (e.CurrentSelection.FirstOrDefault() is Veicolo veicoloSelezionato)
        //{
        //    // Naviga alla pagina di dettaglio con il repository
        //    var veicoloViewModel = (VeicoliViewModel)BindingContext;
        //    var repository = ((VeicoliViewModel)BindingContext)._veicoloRepository;
        //    await Navigation.PushAsync(new VeicoloDettaglioPage
        //    {
        //        BindingContext = new VeicoloDettaglioViewModel(veicoloSelezionato, repository, veicoloViewModel)
        //    });

        //    // Deseleziona l'elemento per evitare selezioni persistenti
        //    ((CollectionView)sender).SelectedItem = null;
        //}
        if (e.CurrentSelection.FirstOrDefault() is Veicolo veicoloSelezionato)
        {
            // Mostra un dialogo per scegliere l'azione
            string azione = await DisplayActionSheet(
                "Scegli un'azione",
                "Annulla",
                null,
                "Modifica",
                "Elimina"
            );

            switch (azione)
            {
                case "Modifica":
                    // Naviga alla pagina di dettaglio
                    //var repository = ((VeicoliViewModel)BindingContext)._veicoloRepository;
                    await Navigation.PushAsync(new VeicoloDettaglioPage
                    {
                        BindingContext = new VeicoloDettaglioViewModel(veicoloSelezionato, _veicoliViewModel._veicoloRepository, _veicoliViewModel)
                    });
                    break;

                case "Elimina":
                    // Esegui il comando di eliminazione
                    _veicoliViewModel.EliminaCommand.Execute(veicoloSelezionato);
                    break;

                default:
                    // Deseleziona l'elemento se l'utente annulla
                    ((CollectionView)sender).SelectedItem = null;
                    break;
            }
        }
    }
}