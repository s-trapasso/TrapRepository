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
        public bool DatiCaricati { get; private set; } = false;

        #region PROPRIETÀ
        [ObservableProperty]
        public ObservableCollection<Veicolo> _veicoli = new ObservableCollection<Veicolo>();

        [ObservableProperty]
        private Veicolo _newVeicolo = new Veicolo();

        [ObservableProperty]
        private Proprietario _proprietario = new Proprietario();

        [ObservableProperty]
        public ObservableCollection<Proprietario> proprietari = new ObservableCollection<Proprietario>();

        [ObservableProperty]
        private Proprietario selectedProprietario;
        [ObservableProperty]
        public ObservableCollection<Manutenzione> manutenzioni = new ObservableCollection<Manutenzione>();
        #endregion
        public bool MostraFormNuovoProprietario => SelectedProprietario == null;
        partial void OnSelectedProprietarioChanged(Proprietario value)
        {
            OnPropertyChanged(nameof(MostraFormNuovoProprietario));
        }

        public List<AlimentazioneEnum> AlimentazioniDisponibili { get; } = Enum.GetValues(typeof(AlimentazioneEnum)).Cast<AlimentazioneEnum>().ToList();
        public VeicoloViewModel(IRepositoryManager repositoryManager, ILoggingServiceFactory loggingServiceFactory, NavigationService navigationService)
        {
            _repositoryManager = repositoryManager;
            _logger = loggingServiceFactory.CreateLogger<VeicoloViewModel>();
            _navigationService = navigationService;
        }

        #region Command Aggiungi Veicolo / Proprietario
        [RelayCommand]
        private async Task AggiungiAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewVeicolo.Targa) ||
                    string.IsNullOrWhiteSpace(NewVeicolo.Marca) ||
                    string.IsNullOrWhiteSpace(NewVeicolo.Modello))
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "I campi del veicolo sono obbligatori.", "OK");
                    return;
                }

                bool nuovoProprietarioValido = Proprietario != null &&
                    !string.IsNullOrWhiteSpace(Proprietario.Nome) &&
                    !string.IsNullOrWhiteSpace(Proprietario.Cognome) &&
                    !string.IsNullOrWhiteSpace(Proprietario.Indirizzo);

                if (SelectedProprietario == null && !nuovoProprietarioValido)
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Seleziona un proprietario esistente o inseriscine uno nuovo.", "OK");
                    return;
                }

                var veicoloEsistente = await _repositoryManager.Veicoli.GetVeicoloByTargaAsync(NewVeicolo.Targa);
                if (veicoloEsistente != null)
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Il veicolo esiste già.", "OK");
                    return;
                }

                if (SelectedProprietario != null)
                {
                    NewVeicolo.ProprietarioId = SelectedProprietario.Id;
                }
                else
                {
                    var proprietarioEsistente = await _repositoryManager.Proprietari
                        .GetProprietarioByNomeCognomeAsync(Proprietario.Nome, Proprietario.Cognome);

                    if (proprietarioEsistente != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Info", "Il proprietario esiste già. Verrà associato al veicolo.", "OK");
                        NewVeicolo.ProprietarioId = proprietarioEsistente.Id;
                    }
                    else
                    {
                        await _repositoryManager.Proprietari.AddProprietarioAsync(Proprietario);
                        NewVeicolo.ProprietarioId = Proprietario.Id;
                        await _repositoryManager.Proprietari.GetAllProprietariAsync();
                    }
                }

                await _repositoryManager.Veicoli.AddVeicoloAsync(NewVeicolo);

                // Ricarica la lista veicoli dal DB per avere dati aggiornati (es. ID)
                // Ricarica la lista proprietari per aggiornare il picker
                await LoadDataAsync();

                

                NewVeicolo = new Veicolo();
                Proprietario = new Proprietario();
                SelectedProprietario = null;
                _logger.Info("Nuovo veicolo e proprietario aggiunti.");
                
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante l'aggiunta del veicolo", ex);
                await App.Current.MainPage.DisplayAlert("Errore", "Si è verificato un errore durante il salvataggio.", "OK");
            }
        }
        #endregion
        #region Elimina Veicolo
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
        #endregion
         
        [RelayCommand]
        private async Task SelezionaVeicoloAsync(Veicolo veicolo)
        {
            if (veicolo == null)
            {
                _logger.Warn("Veicolo passato a SelezionaVeicoloAsync è null.");
                return;
            }
            await Shell.Current.GoToAsync($"veicoloDettaglio?id={veicolo.Id}");
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
        public async Task LoadDataAsync()
        {
            var result = await _repositoryManager.CaricaDatiInizialiAsync();
            Proprietari = new ObservableCollection<Proprietario>(result.proprietari);
            Veicoli = new ObservableCollection<Veicolo>(result.veicoli);
            Manutenzioni = new ObservableCollection<Manutenzione>(result.manutenzioni);
        }
    }
}
