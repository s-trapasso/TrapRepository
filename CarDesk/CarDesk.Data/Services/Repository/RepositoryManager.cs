using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Data;
using CarDesk.Data.DTOs;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace CarDesk.Data.Services.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        //private readonly CarDeskDbContext _context;
        //private readonly ILoggingService<RepositoryManager> _logger;

        //public IVeicoliRepository Veicoli { get; }
        //public IProprietarioRepository Proprietari { get; }
        //public IManutenzioneRepository Manutenzioni { get; }

        //public RepositoryManager(
        //    IVeicoliRepository veicoli,
        //    IProprietarioRepository proprietari,
        //    IManutenzioneRepository manutenzioni,
        //    ILoggingService<RepositoryManager> logger)
        //{
        //    Veicoli = veicoli;
        //    Proprietari = proprietari;
        //    Manutenzioni = manutenzioni;
        //    _logger = logger;

        //    _logger.LogInformation("RepositoryManager creato");
        //}
        //public async Task<(List<Veicolo> veicoli, List<Proprietario> proprietari, List<Manutenzione> manutenzioni)> CaricaDatiInizialiAsync()
        //{
        //    _logger.LogInformation("Caricamento dati iniziali...");
        //    var manutenzioniTask = await Manutenzioni.GetAllManutenzioniAsync();
        //    var proprietariTask = await Proprietari.GetAllProprietariAsync();
        //    var veicoliTask = await Veicoli.GetVeicoliAsync();

        //    _logger.LogInformation("Caricati : {VeicoliCount} veicoli, {ProprietariCount} proprietari, {ManutenzioniCount} manutenzioni", veicoliTask.Count,proprietariTask.Count,manutenzioniTask.Count);

        //        return (veicoliTask, proprietariTask, manutenzioniTask);
        //}
        private readonly IServiceProvider _provider;
        private readonly ILogger<RepositoryManager> _logger;

        public RepositoryManager(IServiceProvider provider, ILogger<RepositoryManager> logger)
        {
            _provider = provider;
            _logger = logger;
            _logger.LogInformation("RepositoryManager creato");
        }
        public IGenericRepository<T> For<T>() where T : class
        {
            return _provider.GetRequiredService<IGenericRepository<T>>();
        }
        public async Task<DatiInizialiDto> CaricaDatiInizialiAsync()
        {
            _logger.LogInformation("Caricamento dati iniziali...");
            var veicoliRepository = For<Veicolo>();
            var proprietariRepository = For<Proprietario>();
            var manutenzioniRepository = For<Manutenzione>();

            var veicoliTask = veicoliRepository.GetAllAsync();
            var proprietariTask = proprietariRepository.GetAllAsync();
            var manutenzioniTask = manutenzioniRepository.GetAllAsync();

            await Task.WhenAll(veicoliTask, proprietariTask, manutenzioniTask);

            var elencoVeicoli = veicoliTask.Result;
            var elencoProprietari = proprietariTask.Result;
            var elencoManutenzioni = manutenzioniTask.Result;

            var targaId = elencoVeicoli.ToDictionary(v => v.Id);
            var proprietarioId = elencoProprietari.ToDictionary(p => p.Id);

            foreach(var v in elencoVeicoli)
            {
                if (proprietarioId.TryGetValue(v.ProprietarioId, out var proprietario))
                {
                    v.Proprietario = proprietario;
                }
            }

            foreach (var m in elencoManutenzioni)
            {
                if(targaId.TryGetValue(m.VeicoloId, out var veicolo))
                {
                    m.Veicolo = veicolo;
                }
            }

            var dati = new DatiInizialiDto
            {
                Veicoli = elencoVeicoli,
                Proprietari = elencoProprietari,
                Manutenzioni = elencoManutenzioni,
            };

            _logger.LogInformation("Dati iniziali caricati: Veicoli={VeicoliCount}, Proprietari={ProprietariCount}, Manutenzioni={ManutenzioniCount}",
                dati.Veicoli.Count, dati.Proprietari.Count, dati.Manutenzioni.Count);

            return dati;
        }
    }
}
