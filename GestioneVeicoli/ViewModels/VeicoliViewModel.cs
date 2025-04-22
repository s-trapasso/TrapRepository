using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GestioneVeicoli.Models;
using GestioneVeicoli.Services;
using GestioneVeicoli.Views;

namespace GestioneVeicoli.ViewModels
{
    public class VeicoliViewModel : INotifyPropertyChanged
    {
        public readonly IVeicoliRepository _veicoloRepository;

        #region COMMAND
        public ICommand CaricaCommand { get; }
        public ICommand AggiungiCommand { get; }
        public ICommand SelezionaCommand { get; }
        public ICommand EliminaCommand { get; }
        #endregion
        public ObservableCollection<Veicolo> Veicoli { get; set; } = new ObservableCollection<Veicolo>();

        private Veicolo _newVeicolo = new Veicolo();
        

        public Veicolo NewVeicolo
        {
            get => _newVeicolo;
            set => _newVeicolo = value;
        }
        public VeicoliViewModel(IVeicoliRepository veicoloRepository)
        {
            _veicoloRepository = veicoloRepository;

            CaricaCommand = new Command(async () => await CaricaAsync());
            AggiungiCommand = new Command(AggiungiVeicolo);
            SelezionaCommand = new Command<Veicolo>(async (veicolo) => await DettaglioVeicoloAsync(veicolo));
            EliminaCommand = new Command<Veicolo>(EliminaVeicolo);

            
            //Carica veicoli all'avvio
            Task.Run(async () =>
            {
                await CaricaAsync();
            });

        }
        private async Task CaricaAsync()
        {
            Veicoli.Clear();
            var lista = await _veicoloRepository.GetVeicoliAsync();
            foreach (var v in lista)
                Veicoli.Add(v);
        }

        private void AggiungiVeicolo()
        {
            if (!string.IsNullOrWhiteSpace(NewVeicolo.Targa) &&
                !string.IsNullOrWhiteSpace(NewVeicolo.Marca) &&
                !string.IsNullOrWhiteSpace(NewVeicolo.Modello))
            {
                Veicoli.Add(new Veicolo
                {
                    Targa = NewVeicolo.Targa,
                    Marca = NewVeicolo.Marca,
                    Modello = NewVeicolo.Modello,
                    Anno = NewVeicolo.Anno
                });

                // Resetta il veicolo per il prossimo inserimento
                NewVeicolo = new Veicolo();

            }
        }

        private async Task DettaglioVeicoloAsync(Veicolo veicolo)
        {
            if(veicolo is null)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new VeicoloDettaglioPage
            {
                BindingContext = new VeicoloDettaglioViewModel(veicolo,_veicoloRepository,this)
            });
        }

        private void EliminaVeicolo(Veicolo veicoloSelezionato)
        {
            if (veicoloSelezionato is null)
                return;
            Veicoli.Remove(veicoloSelezionato);
            // Chiamata al repository per eliminare il veicolo
            _ = _veicoloRepository.DeleteVeicoloAsync(veicoloSelezionato.Id);
        }


        // Aggiungi comandi come AddCommand, DeleteCommand ecc.
        // Implementa INotifyPropertyChanged come di consueto
        public event PropertyChangedEventHandler PropertyChanged;
    }

}
