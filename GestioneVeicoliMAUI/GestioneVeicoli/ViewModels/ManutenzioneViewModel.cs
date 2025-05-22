using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using GestioneVeicoli.Log;

namespace GestioneVeicoli.ViewModels
{
    public partial class ManutenzioneViewModel : ObservableObject
    {
        private readonly IRepositoryManager _repositoryManager;
       
        private readonly ILoggingService _logger;

        [ObservableProperty]
        private ObservableCollection<Manutenzione> manutenzioni = new();

        [ObservableProperty]
        private Manutenzione nuovaManutenzione = new();

        private int veicoloId;

        public ManutenzioneViewModel(IRepositoryManager repositoryManager, ILoggingServiceFactory loggerFactory)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerFactory.CreateLogger<ManutenzioneViewModel>();
        }

        public void SetVeicoloId(int id)
        {
            veicoloId = id;
            NuovaManutenzione = new Manutenzione { VeicoloId = id, Data = DateTime.Today };
            _ = CaricaManutenzioniAsync();
        }

        [RelayCommand]
        private async Task CaricaManutenzioniAsync()
        {
            try
            {
                Manutenzioni.Clear();
                var lista = await _repositoryManager.Manutenzioni.GetByVeicoloIdAsync(veicoloId);
                foreach (var m in lista)
                    Manutenzioni.Add(m);
                _logger.Info($"Caricate {lista.Count} manutenzioni per il veicolo {veicoloId}");
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante il caricamento delle manutenzioni", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task AggiungiManutenzioneAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NuovaManutenzione.TipoIntervento))
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Tipo intervento obbligatorio.", "OK");
                    return;
                }

                await _repositoryManager.Manutenzioni.AddAsync(NuovaManutenzione);
                Manutenzioni.Add(NuovaManutenzione);

                _logger.Info("Manutenzione aggiunta con successo");

                NuovaManutenzione = new Manutenzione { VeicoloId = veicoloId, Data = DateTime.Today };
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante l'aggiunta della manutenzione", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task EliminaManutenzioneAsync(Manutenzione manutenzione)
        {
            try
            {
                if (manutenzione == null) return;
               
                await _repositoryManager.Manutenzioni.DeleteAsync(manutenzione.Id);
                Manutenzioni.Remove(manutenzione);
                _logger.Info($"Manutenzione {manutenzione.Id} eliminata");
            }
            catch (Exception ex)
            {
                _logger.Error("Errore durante l'eliminazione della manutenzione", ex);
                await App.Current.MainPage.DisplayAlert("Errore", ex.Message, "OK");
            }
        }
    }

}
