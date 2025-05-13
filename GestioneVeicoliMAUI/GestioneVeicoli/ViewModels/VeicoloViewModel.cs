using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using GestioneVeicoli.Log;

namespace GestioneVeicoli.ViewModels
{
    public partial class VeicoloViewModel : ObservableObject //INotifyPropertyChanged
    {
        public readonly IVeicoliRepository _veicoloRepository;
        public readonly IProprietarioRepository _proprietarioRepository;
        public readonly ILoggingService _logger;
        public readonly NavigationService _navigationService;

        [ObservableProperty]
        public ObservableCollection<Veicolo> _veicoli = new ObservableCollection<Veicolo>();

        [ObservableProperty]
        private Veicolo _newVeicolo = new Veicolo();

        [ObservableProperty]
        private Proprietario _proprietario = new Proprietario();

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
        public VeicoloViewModel(IVeicoliRepository veicoloRepository, IProprietarioRepository proprietarioRepository,
            ILoggingServiceFactory loggingServiceFactory, NavigationService navigationService)
        {
            _veicoloRepository = veicoloRepository;
            _proprietarioRepository = proprietarioRepository;
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
                // 1. Validazione campi obbligatori
                if (string.IsNullOrWhiteSpace(NewVeicolo.Targa) ||
                    string.IsNullOrWhiteSpace(NewVeicolo.Marca) ||
                    string.IsNullOrWhiteSpace(NewVeicolo.Modello) ||
                    string.IsNullOrWhiteSpace(Proprietario.Nome) ||
                    string.IsNullOrWhiteSpace(Proprietario.Cognome) ||
                    string.IsNullOrWhiteSpace(Proprietario.Indirizzo))
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Tutti i campi sono obbligatori.", "OK");
                    return;
                }

                // 2. Controllo se il proprietario esiste già
                var proprietarioEsistente = await _proprietarioRepository
                    .GetProprietarioByDetailsAsync(Proprietario.Nome, Proprietario.Cognome, Proprietario.Indirizzo);

                if (proprietarioEsistente != null)
                {
                    _logger.Info("Proprietario già esistente.");
                    NewVeicolo.ProprietarioId = proprietarioEsistente.Id;
                }
                else
                {
                    await _proprietarioRepository.AddProprietarioAsync(Proprietario);
                    NewVeicolo.ProprietarioId = Proprietario.Id;
                }

                // 3. Controllo se esiste già un veicolo con la stessa targa
                var veicoloEsistente = await _veicoloRepository
                    .GetVeicoloByTargaAsync(NewVeicolo.Targa);

                if (veicoloEsistente != null)
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Veicolo già esistente con la stessa targa.", "OK");
                    _logger.Info("Veicolo già esistente con la stessa targa.");
                    return;
                }

                // 4. Inserimento nuovo veicolo
                await _veicoloRepository.AddVeicoloAsync(NewVeicolo);
                Veicoli.Add(NewVeicolo);

                // 5. Reset modelli
                NewVeicolo = new Veicolo();
                Proprietario = new Proprietario();

                _logger.Info("Nuovo veicolo e (eventualmente) nuovo proprietario aggiunti.");
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
        private async Task SelezionaVeicoloAsync(Veicolo veicolo)
        {
            if (veicolo == null)
            {
                _logger.Warn("Veicolo passato a SelezionaVeicoloAsync è null.");
                return;
            }
            await _navigationService.NavigateToDettaglioAsync(veicolo);
        }

        [RelayCommand]
        private async Task SelezionaProprietarioAsync(Veicolo veicolo)
        {
            if (veicolo == null)
            {
                _logger.Warn("Veicolo passato a SelezionaProprietarioAsync è null.");
                return;
            }
            await _navigationService.NavigateToDettaglioAsync(veicolo);
        }
    }
}
