using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;

namespace GestioneVeicoli.ViewModels
{
    public class VeicoloDettaglioViewModel
    {
        private readonly IVeicoliRepository _veicoloRepository;
        private readonly VeicoloViewModel _VeicoloViewModel;
        public Veicolo Veicolo { get; set; }
        public ICommand SalvaCommand { get; }
        public VeicoloDettaglioViewModel(Veicolo veicolo, IVeicoliRepository veicoliRepository, VeicoloViewModel VeicoloViewModel)
        {
            Veicolo = veicolo;
            _veicoloRepository = veicoliRepository;
            _VeicoloViewModel = VeicoloViewModel;

            SalvaCommand = new Command(async () => await SalvaVeicoloAsync());
        }

        private async Task SalvaVeicoloAsync()
        {
            
            try
            {
                // Salva le modifiche nel repository
                await _veicoloRepository.UpdateVeicoloAsync(Veicolo);

                // Aggiorna direttamente l'elemento nella lista
                var veicoloDaAggiornare = _VeicoloViewModel.Veicoli.FirstOrDefault(v => v.Id == Veicolo.Id);
                if (veicoloDaAggiornare != null)
                {
                    veicoloDaAggiornare.Targa = Veicolo.Targa;
                    veicoloDaAggiornare.Marca = Veicolo.Marca;
                    veicoloDaAggiornare.Modello = Veicolo.Modello;
                    veicoloDaAggiornare.Anno = Veicolo.Anno;
                }

                // Torna alla pagina principale
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Gestisci l'eccezione (es. mostra un messaggio di errore)
                await App.Current.MainPage.DisplayAlert("Errore", $"Si è verificato un errore durante il salvataggio del veicolo\n Errore {ex.Message}.", "OK");
            }
            
        }
    }
}
