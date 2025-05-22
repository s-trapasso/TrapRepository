using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using GestioneVeicoli.Log;

namespace GestioneVeicoli.ViewModels
{
    public partial class ProprietarioViewModel : ObservableObject
    {
        private readonly IProprietarioRepository _proprietarioRepository;
        private readonly ILoggingService _logger;

        [ObservableProperty]
        private ObservableCollection<Proprietario> _proprietari = new ObservableCollection<Proprietario>();

        [ObservableProperty]
        private Proprietario _nuovoProprietario = new();

        public ProprietarioViewModel(IProprietarioRepository proprietarioRepository, ILoggingServiceFactory loggingServiceFactory)
        {
            _proprietarioRepository = proprietarioRepository;
            _logger = loggingServiceFactory.CreateLogger<ProprietarioViewModel>();
            _ = CaricaProprietariAsync();
        }

        [RelayCommand]
        private async Task CaricaProprietariAsync()
        {
            try
            {
                Proprietari.Clear();
                var lista = await _proprietarioRepository.GetAllProprietariAsync();
                foreach (var p in lista)
                    Proprietari.Add(p);
                _logger.Info($"Caricati {Proprietari.Count} proprietari");
            }
            catch (Exception ex)
            {
                _logger.Error("Errore nel caricamento dei proprietari", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task AggiungiProprietarioAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NuovoProprietario.Nome) ||
                    string.IsNullOrWhiteSpace(NuovoProprietario.Cognome) ||
                    string.IsNullOrWhiteSpace(NuovoProprietario.Indirizzo))
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Tutti i campi sono obbligatori.", "OK");
                    return;
                }

                await _proprietarioRepository.AddProprietarioAsync(NuovoProprietario);
                Proprietari.Add(NuovoProprietario);
                _logger.Info("Proprietario aggiunto con successo");

                NuovoProprietario = new Proprietario();
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante l'aggiunta del proprietario", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task EliminaProprietarioAsync(Proprietario proprietario)
        {
            try
            {
                if (proprietario == null) return;
                await _proprietarioRepository.DeleteProprietarioAsync(proprietario.Id);
                Proprietari.Remove(proprietario);
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante l'eliminazione del proprietario", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }
    }
}