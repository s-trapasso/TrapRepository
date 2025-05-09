using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestioneVeicoli.Log;
using GestioneVeicoli.Models;
using GestioneVeicoli.Services;
using GestioneVeicoli.Services.VeicoloRepository;
using GestioneVeicoli.Views;
using log4net.Repository.Hierarchy;

namespace GestioneVeicoli.ViewModels
{
    public partial class VeicoloViewModel : ObservableObject //INotifyPropertyChanged
    {
        public readonly IVeicoliRepository _veicoloRepository;
        public readonly ILoggingService _logger;
        public readonly NavigationService _navigationService;

        [ObservableProperty]
        public ObservableCollection<Veicolo> _veicoli = new ObservableCollection<Veicolo>();

        [ObservableProperty]
        private Veicolo _newVeicolo = new Veicolo();


        //#region COMMAND
        //public ICommand CaricaCommand { get; }
        //public ICommand AggiungiCommand { get; }
        //public ICommand SelezionaCommand { get; }
        //public ICommand EliminaCommand { get; }
        //#endregion



        //public Veicolo NewVeicolo
        //{
        //    get => _newVeicolo;
        //    set
        //    {
        //        if (_newVeicolo != value)
        //        {
        //            _newVeicolo = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewVeicolo)));
        //        }
        //    }
        //}
        public VeicoloViewModel(IVeicoliRepository veicoloRepository,ILoggingServiceFactory loggingServiceFactory, NavigationService navigationService)
        {
            _veicoloRepository = veicoloRepository;
            _logger = loggingServiceFactory.CreateLogger<VeicoloViewModel>();
            _navigationService = navigationService;
            // Inizializza i comandi
            _ = CaricaVeicoliAsync();

            //CaricaCommand = new Command(async () => await CaricaVeicoliAsync());
            //AggiungiCommand = new Command(async () => await AggiungiVeicoloAsync());
            //EliminaCommand = new Command<Veicolo>(async (veicolo) => await EliminaVeicoloAsync(veicolo));

            //// Carica i veicoli all'avvio
            //Task.Run(async () => await CaricaVeicoliAsync());

        }

        [RelayCommand]
        private async Task CaricaVeicoliAsync()
        {
            try
            {
                Veicoli.Clear();
                var lista = await _veicoloRepository.GetVeicoliAsync();
                foreach (var v in lista)
                    Veicoli.Add(v);
                _logger.Info($"Caricati {Veicoli.Count} veicoli");
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante il caricamento dei veicoli", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task AggiungiAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewVeicolo.Targa) ||
                    string.IsNullOrWhiteSpace(NewVeicolo.Marca) ||
                    string.IsNullOrWhiteSpace(NewVeicolo.Modello))
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Tutti i campi sono obbligatori.", "OK");
                    return;
                }

                await _veicoloRepository.AddVeicoloAsync(NewVeicolo);
                Veicoli.Add(NewVeicolo);
                NewVeicolo = new Veicolo(); // reset
                _logger.Info("Veicolo aggiunto");
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante l'aggiunta del veicolo", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task EliminaAsync(Veicolo veicolo)
        {
            try
            {
                if (veicolo == null) return;
                await _veicoloRepository.DeleteVeicoloAsync(veicolo.Id);
                Veicoli.Remove(veicolo);
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante l'eliminazione del veicolo", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }
                
        [RelayCommand]
        private async Task SelezionaAsync(Veicolo veicolo)
        {
            if (veicolo == null)
            {
                _logger.Warn("Veicolo passato a SelezionaAsync è null.");
                return;
            }
            await _navigationService.NavigateToDettaglioAsync(veicolo);
        }
    }
    //private async Task AggiungiVeicoloAsync()
    //{
    //    try
    //    {
    //        if (string.IsNullOrWhiteSpace(NewVeicolo.Targa) ||
    //            string.IsNullOrWhiteSpace(NewVeicolo.Marca) ||
    //            string.IsNullOrWhiteSpace(NewVeicolo.Modello))
    //        {
    //            await App.Current.MainPage.DisplayAlert("Errore", "Tutti i campi sono obbligatori.", "OK");
    //            return;
    //        }
    //        else
    //        {
    //            await _veicoloRepository.AddVeicoloAsync(NewVeicolo);
    //            Veicoli.Add(NewVeicolo);
    //            NewVeicolo = new Veicolo(); // Resetta il modello
    //            _logger.Info($"Veicolo aggiunto");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.Error("Errore durante l'aggiunta del veicolo", ex);
    //        await App.Current.MainPage.DisplayAlert("Errore", $"Si è verificato un errore durante l'aggiunta del veicolo\n Errore {ex.Message}.", "OK");
    //    }
    //}

    //private async Task DettaglioVeicoloAsync(Veicolo veicolo)
    //{
    //    if(veicolo is null)
    //        return;

    //    await App.Current.MainPage.Navigation.PushAsync(new VeicoloDettaglioPage
    //    {
    //        BindingContext = new VeicoloDettaglioViewModel(veicolo,_veicoloRepository,this)
    //    });
    //}

    //private async Task EliminaVeicoloAsync(Veicolo veicoloSelezionato)
    //{
    //    try 
    //    {
    //        if (veicoloSelezionato != null)
    //        {
    //            await _veicoloRepository.DeleteVeicoloAsync(veicoloSelezionato.Id);
    //            Veicoli.Remove(veicoloSelezionato);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.Error("Errore durante l'eliminazione del veicolo", ex);
    //        await App.Current.MainPage.DisplayAlert("Errore", $"Si è verificato un errore durante l'eliminazione del veicolo\n Errore {ex.Message}.", "OK");
    //    }
    //}


    //// Aggiungi comandi come AddCommand, DeleteCommand ecc.
    //// Implementa INotifyPropertyChanged come di consueto
    //public event PropertyChangedEventHandler PropertyChanged;

}
