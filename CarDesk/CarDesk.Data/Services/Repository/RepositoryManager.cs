using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using CarDesk.Data.Services.VeicoloRepository;

namespace CarDesk.Data.Services.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CarDeskDbContext _context;

        public IVeicoliRepository Veicoli { get; }
        public IProprietarioRepository Proprietari { get; }
        public IManutenzioneRepository Manutenzioni { get; }

        public RepositoryManager(CarDeskDbContext context)
        {
            _context = context;
            Veicoli = new VeicoliRepository(_context);
            Proprietari = new ProprietarioRepository(_context);
            Manutenzioni = new ManutenzioneRepository(_context);
        }

        public async Task<(List<Proprietario> proprietari, List<Veicolo> veicoli, List<Manutenzione> manutenzioni)> CaricaDatiInizialiAsync()
        {
            var manutenzioniTask = Manutenzioni.GetAllManutenzioniAsync();
            var proprietariTask = Proprietari.GetAllProprietariAsync();
            var veicoliTask = Veicoli.GetVeicoliAsync();
            await Task.WhenAll(manutenzioniTask, proprietariTask, veicoliTask);
            return (proprietariTask.Result, veicoliTask.Result, manutenzioniTask.Result);
        }
    }
}
