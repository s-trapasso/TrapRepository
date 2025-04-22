using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GestioneVeicoli.Models;
using GestioneVeicoli.Services;

namespace GestioneVeicoli.ViewModels
{
    public class VeicoloDettaglioViewModel
    {
        private readonly IVeicoliRepository _veicoloRepository;
        private readonly VeicoliViewModel _veicoliViewModel;
        public Veicolo Veicolo { get; set; }
        public ICommand SalvaCommand { get; }
        public VeicoloDettaglioViewModel(Veicolo veicolo, IVeicoliRepository veicoliRepository, VeicoliViewModel veicoliViewModel)
        {
            Veicolo = veicolo;
            _veicoloRepository = veicoliRepository;
            _veicoliViewModel = veicoliViewModel;

            SalvaCommand = new Command(async () => await SalvaVeicoloAsync());
        }

        private async Task SalvaVeicoloAsync()
        {
            // Salva le modifiche nel repository
            await _veicoloRepository.UpdateVeicoloAsync(Veicolo);

            // Aggiorna direttamente l'elemento nella lista
            var veicoloDaAggiornare = _veicoliViewModel.Veicoli.FirstOrDefault(v => v.Id == Veicolo.Id);
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
    }
}
