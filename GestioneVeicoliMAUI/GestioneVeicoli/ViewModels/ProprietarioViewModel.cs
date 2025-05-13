using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using System.Windows.Input;
using GestioneVeicoli.Data.Services.VeicoloRepository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using GestioneVeicoli.Log;

namespace GestioneVeicoli.ViewModels
{
    public partial class ProprietarioViewModel : ObservableObject //INotifyPropertyChanged
    {
        public readonly IProprietarioRepository _proprietarioRepository;
        public readonly ILoggingService _logger;
        public readonly NavigationService _navigationService;

        [ObservableProperty]
        public ObservableCollection<Proprietario> _proprietari = new ObservableCollection<Proprietario>();

        [ObservableProperty]
        private Proprietario _newProprietario = new Proprietario();

        [ObservableProperty]
        private bool isEditMode;

        public ProprietarioViewModel(IProprietarioRepository proprietarioRepository, 
            ILoggingServiceFactory loggingServiceFactory, 
            NavigationService navigationService)
        {
            _proprietarioRepository = proprietarioRepository;
            _logger = loggingServiceFactory.CreateLogger<ProprietarioViewModel>();
            _navigationService = navigationService;
            // Inizializza i comandi
            _ = CaricaProprietariAsync();
        }
        /// <summary>
        /// Inizializza il ViewModel per la modifica di un proprietario esistente.
        /// </summary>
        public void Initialize(Proprietario proprietario)
        {
            if (proprietario == null) return;

            NewProprietario = new Proprietario
            {
                Id = proprietario.Id,
                Nome = proprietario.Nome,
                Cognome = proprietario.Cognome,
                Indirizzo = proprietario.Indirizzo
            };

            IsEditMode = true;
        }

        [RelayCommand]
        private async Task CaricaProprietariAsync()
        {
            try
            {
                Proprietari.Clear();
                var lista = await _proprietarioRepository.GetAllProprietariAsync();
                foreach (var v in lista)
                    Proprietari.Add(v);
                _logger.Info($"Caricati {Proprietari.Count} proprietari");
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante il caricamento dei proprietari", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task SalvaProprietarioAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewProprietario.Nome) ||
                    string.IsNullOrWhiteSpace(NewProprietario.Cognome) ||
                    string.IsNullOrWhiteSpace(NewProprietario.Indirizzo))
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Tutti i campi sono obbligatori.", "OK");
                    return;
                }

                if (IsEditMode)
                {
                    await _proprietarioRepository.UpdateProprietarioAsync(NewProprietario);
                    _logger.Info($"Proprietario aggiornato con ID {NewProprietario.Id}");
                }
                else
                {
                    var proprietarioEsistente = await _proprietarioRepository
                        .GetProprietarioByDetailsAsync(NewProprietario.Nome, NewProprietario.Cognome, NewProprietario.Indirizzo);

                    if (proprietarioEsistente != null)
                    {
                        _logger.Info("Proprietario già esistente. Operazione annullata.");
                        await App.Current.MainPage.DisplayAlert("Attenzione", "Proprietario già esistente.", "OK");
                        return;
                    }

                    await _proprietarioRepository.AddProprietarioAsync(NewProprietario);
                    Proprietari.Add(NewProprietario);
                    _logger.Info("Nuovo proprietario aggiunto.");
                }

                // Reset e ritorno
                NewProprietario = new Proprietario();
                IsEditMode = false;

                await _navigationService.NavigateBackAsync();
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante il salvataggio del proprietario", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task EliminaProprietarioAsync(Proprietario proprietario)
        {
            try
            {
                if (proprietario == null)
                    return;

                bool conferma = await App.Current.MainPage.DisplayAlert("Conferma", "Eliminare il proprietario?", "Sì", "No");
                if (!conferma) return;

                await _proprietarioRepository.DeleteProprietarioAsync(proprietario.Id);
                Proprietari.Remove(proprietario);
                _logger.Info($"Proprietario ID {proprietario.Id} eliminato.");
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante l'eliminazione del proprietario", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }
    }
}