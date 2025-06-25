using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace CarDesk.Data.Services.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        //private readonly CarDeskDbContext _context;
        private readonly ILoggingService<RepositoryManager> _logger;

        public IVeicoliRepository Veicoli { get; }
        public IProprietarioRepository Proprietari { get; }
        public IManutenzioneRepository Manutenzioni { get; }

        public RepositoryManager(
            IVeicoliRepository veicoli,
            IProprietarioRepository proprietari,
            IManutenzioneRepository manutenzioni,
            ILoggingService<RepositoryManager> logger)
        {
            Veicoli = veicoli;
            Proprietari = proprietari;
            Manutenzioni = manutenzioni;
            _logger = logger;

            _logger.LogInformation("RepositoryManager creato");
        }
        public async Task<(List<Veicolo> veicoli, List<Proprietario> proprietari, List<Manutenzione> manutenzioni)> CaricaDatiInizialiAsync()
        {
            _logger.LogInformation("Caricamento dati iniziali...");
            var manutenzioniTask = await Manutenzioni.GetAllManutenzioniAsync();
            var proprietariTask = await Proprietari.GetAllProprietariAsync();
            var veicoliTask = await Veicoli.GetVeicoliAsync();

            _logger.LogInformation("Caricati : {VeicoliCount} veicoli, {ProprietariCount} proprietari, {ManutenzioniCount} manutenzioni", veicoliTask.Count,proprietariTask.Count,manutenzioniTask.Count);

                return (veicoliTask, proprietariTask, manutenzioniTask);
        }
    }
}
