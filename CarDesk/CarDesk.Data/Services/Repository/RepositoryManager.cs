using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Data;
using CarDesk.Data.DTOs;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace CarDesk.Data.Services.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
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

            var veicoliTask = For<Veicolo>().GetAllAsync(q => q.Include(v => v.Proprietario));
            var proprietariTask = For<Proprietario>().GetAllAsync();
            var manutenzioniTask = For<Manutenzione>().GetAllAsync(q => q.Include(m => m.Veicolo));


            await Task.WhenAll(veicoliTask, proprietariTask, manutenzioniTask);

            var dati = new DatiInizialiDto
            {
                Veicoli = veicoliTask.Result,
                Proprietari = proprietariTask.Result,
                Manutenzioni = manutenzioniTask.Result
            };

            _logger.LogInformation("Dati iniziali caricati: Veicoli={0}, Proprietari={1}, Manutenzioni={2}",
                dati.Veicoli.Count, dati.Proprietari.Count, dati.Manutenzioni.Count);

            return dati;
        }
    }
}
