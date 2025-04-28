using GestioneVeicoli.Models;
using GestioneVeicoli.Services;
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
        //Questo codice fa in modo che alla selezione di un veicolo, venga aperta la pagina di dettaglio
        //if (e.CurrentSelection.FirstOrDefault() is Veicolo veicoloSelezionato)
        //{
        //    // Naviga alla pagina di dettaglio con il repository
        //    var veicoloViewModel = (VeicoloViewModel)BindingContext;
        //    var repository = ((VeicoloViewModel)BindingContext)._veicoloRepository;
        //    await Navigation.PushAsync(new VeicoloDettaglioPage
        //    {
        //        BindingContext = new VeicoloDettaglioViewModel(veicoloSelezionato, repository, veicoloViewModel)
        //    });

        //    // Deseleziona l'elemento per evitare selezioni persistenti
        //    ((CollectionView)sender).SelectedItem = null;
        //}
        try
        {
            if (e.CurrentSelection.FirstOrDefault() is Veicolo veicoloSelezionato)
            {
                string azione = await DisplayActionSheet(
                    "Scegli un'azione",
                    "Annulla",
                    null,
                    "Modifica",
                    "Elimina"
                );

                var navigationService = MauiProgram.ServiceProvider.GetRequiredService<NavigationService>();

                if (azione == "Modifica")
                {
                    await navigationService.NavigateToDettaglioAsync(veicoloSelezionato);
                }
                else if (azione == "Elimina")
                {
                    _VeicoloViewModel.EliminaCommand.Execute(veicoloSelezionato);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Errore", $"Si è verificato un errore: {ex.Message}", "OK");
        }
        finally
        {
            // Deseleziona l'elemento in ogni caso
            ((CollectionView)sender).SelectedItem = null;
        }
    }
    
}