using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using GestioneVeicoli.Log;

namespace GestioneVeicoli.ViewModels
{
    public partial class VeicoloViewModel : ObservableObject
    {
        private readonly IRepositoryManager _repositoryManager;
        public readonly ILoggingService _logger;
        public readonly NavigationService _navigationService;

        [ObservableProperty]
        public ObservableCollection<Veicolo> _veicoli = new ObservableCollection<Veicolo>();

        [ObservableProperty]
        private Veicolo _newVeicolo = new Veicolo();

        [ObservableProperty]
        private Proprietario _proprietario = new Proprietario();

        public List<AlimentazioneEnum> AlimentazioniDisponibili { get; } = Enum.GetValues(typeof(AlimentazioneEnum)).Cast<AlimentazioneEnum>().ToList();

        public VeicoloViewModel(IRepositoryManager repositoryManager, ILoggingServiceFactory loggingServiceFactory, NavigationService navigationService)
        {
            _repositoryManager = repositoryManager;
            _logger = loggingServiceFactory.CreateLogger<VeicoloViewModel>();
            _navigationService = navigationService;
            _ = CaricaVeicoliAsync();
        }

        [RelayCommand]
        private async Task CaricaVeicoliAsync()
        {
            try
            {
                Veicoli.Clear();
                var lista = await _repositoryManager.Veicoli.GetVeicoliAsync();
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
                    string.IsNullOrWhiteSpace(NewVeicolo.Modello) ||
                    string.IsNullOrWhiteSpace(Proprietario.Nome) ||
                    string.IsNullOrWhiteSpace(Proprietario.Cognome) ||
                    string.IsNullOrWhiteSpace(Proprietario.Indirizzo))
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Tutti i campi sono obbligatori.", "OK");
                    return;
                }

                //Controlla se il veicolo esiste già
                var veicoloEsistente = await _repositoryManager.Veicoli.GetVeicoloByTargaAsync(NewVeicolo.Targa);
                if (veicoloEsistente != null)
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Il veicolo esiste già.", "OK");
                    return;
                }
                
                //Controlla se il proprietario esiste già
                var proprietarioEsistente = await _repositoryManager.Proprietari.GetProprietarioByNomeCognomeAsync(Proprietario.Nome, Proprietario.Cognome);
                if (proprietarioEsistente != null)
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Il proprietario esiste già.\nVeicolo nuovo salvato", "OK");
                    Proprietario = proprietarioEsistente;
                    NewVeicolo.ProprietarioId = Proprietario.Id;
                }
                else
                {
                    // Aggiungi nuovo proprietario
                    await _repositoryManager.Proprietari.AddProprietarioAsync(Proprietario);
                    NewVeicolo.ProprietarioId = Proprietario.Id;
                }
                //Salva il veicolo
                await _repositoryManager.Veicoli.AddVeicoloAsync(NewVeicolo);
                Veicoli.Add(NewVeicolo);

                NewVeicolo = new Veicolo();
                Proprietario = new Proprietario();

                _logger.Info("Nuovo veicolo e proprietario aggiunti.");
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
                await _repositoryManager.Veicoli.DeleteVeicoloAsync(veicolo.Id);
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
