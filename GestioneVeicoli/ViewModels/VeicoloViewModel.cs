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
    public class VeicoloViewModel : INotifyPropertyChanged
    {
        public readonly IVeicoliRepository _veicoloRepository;
        public ObservableCollection<Veicolo> Veicoli { get; set; } = new ObservableCollection<Veicolo>();
        #region COMMAND
        public ICommand CaricaCommand { get; }
        public ICommand AggiungiCommand { get; }
        public ICommand SelezionaCommand { get; }
        public ICommand EliminaCommand { get; }
        #endregion
       

        private Veicolo _newVeicolo = new Veicolo();
        public Veicolo NewVeicolo
        {
            get => _newVeicolo;
            set
            {
                if (_newVeicolo != value)
                {
                    _newVeicolo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewVeicolo)));
                }
            }
        }
        public VeicoloViewModel(IVeicoliRepository veicoloRepository)
        {
            _veicoloRepository = veicoloRepository;

            CaricaCommand = new Command(async () => await CaricaVeicoliAsync());
            AggiungiCommand = new Command(async () => await AggiungiVeicoloAsync());
            EliminaCommand = new Command<Veicolo>(async (veicolo) => await EliminaVeicoloAsync(veicolo));

            // Carica i veicoli all'avvio
            Task.Run(async () => await CaricaVeicoliAsync());

        }
        private async Task CaricaVeicoliAsync()
        {
            Veicoli.Clear();
            var lista = await _veicoloRepository.GetVeicoliAsync();
            foreach (var v in lista)
                Veicoli.Add(v);
        }

        private async Task AggiungiVeicoloAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewVeicolo.Targa) &&
               !string.IsNullOrWhiteSpace(NewVeicolo.Marca) &&
               !string.IsNullOrWhiteSpace(NewVeicolo.Modello))
            {
                await _veicoloRepository.AddVeicoloAsync(NewVeicolo);
                Veicoli.Add(NewVeicolo);
                NewVeicolo = new Veicolo(); // Resetta il modello
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

        private async Task EliminaVeicoloAsync(Veicolo veicoloSelezionato)
        {
            if (veicoloSelezionato != null)
            {
                await _veicoloRepository.DeleteVeicoloAsync(veicoloSelezionato.Id);
                Veicoli.Remove(veicoloSelezionato);
            }
        }


        // Aggiungi comandi come AddCommand, DeleteCommand ecc.
        // Implementa INotifyPropertyChanged come di consueto
        public event PropertyChangedEventHandler PropertyChanged;
    }

}
