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
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IRepositoryManager _repositoryManager;
        public readonly ILoggingService _logger;
        public readonly NavigationService _navigationService;
       

        #region PROPRIETA'
        
        [ObservableProperty]
        public ObservableCollection<Veicolo> _veicoli = new ObservableCollection<Veicolo>();

        [ObservableProperty]
        public ObservableCollection<Proprietario> _proprietari = new ObservableCollection<Proprietario>();

        [ObservableProperty]
        public ObservableCollection<Manutenzione> _manutenzioni = new ObservableCollection<Manutenzione>();

        [ObservableProperty]
        private int totaleVeicoli;

        [ObservableProperty]
        private int totaleManutenzioni;

        [ObservableProperty]
        private DateTime? prossimaManutenzioneData;

        [ObservableProperty]
        private int totaleProprietari;
        #endregion


        public DashboardViewModel(IRepositoryManager repositoryManager, ILoggingServiceFactory loggingServiceFactory, NavigationService navigationService)
        {
            _repositoryManager = repositoryManager;
            
            _logger = loggingServiceFactory.CreateLogger<VeicoloViewModel>();
            _navigationService = navigationService;
            _= LoadDataAsync();
        }

        

        public async Task LoadDataAsync()
        {
            var result = await _repositoryManager.CaricaDatiInizialiAsync();
            Proprietari = new ObservableCollection<Proprietario>(result.proprietari);
            Veicoli = new ObservableCollection<Veicolo>(result.veicoli);
            Manutenzioni = new ObservableCollection<Manutenzione>(result.manutenzioni);
            prossimaManutenzioneData = Manutenzioni.Where(m => m.Data > DateTime.Today).OrderBy(m => m.Data).Select(m => (DateTime?)m.Data).FirstOrDefault();
        }
    }
}
