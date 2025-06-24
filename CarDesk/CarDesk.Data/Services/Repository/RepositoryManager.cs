using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;


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

        public async Task<(List<Veicolo> veicoli, List<Proprietario> proprietari, List<Manutenzione> manutenzioni)> CaricaDatiInizialiAsync()
        {
            var manutenzioniTask = await Manutenzioni.GetAllManutenzioniAsync();
            var proprietariTask = await Proprietari.GetAllProprietariAsync();
            var veicoliTask = await Veicoli.GetVeicoliAsync();
            return (veicoliTask, proprietariTask, manutenzioniTask);
        }
    }
}
