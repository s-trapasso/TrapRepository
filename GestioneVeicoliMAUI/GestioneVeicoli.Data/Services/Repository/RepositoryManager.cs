using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Data;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using GestioneVeicoli.Data.Services.VeicoloRepository;

namespace GestioneVeicoli.Data.Services.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly VeicoliDbContext _context;

        public IVeicoliRepository Veicoli { get; }
        public IProprietarioRepository Proprietari { get; }
        public IManutenzioneRepository Manutenzioni { get; }

        public RepositoryManager(VeicoliDbContext context)
        {
            _context = context;
            Veicoli = new VeicoliRepository(_context);
            Proprietari = new ProprietarioRepository(_context);
            Manutenzioni = new ManutenzioneRepository(_context);
        }

        public async Task<(List<Proprietario> proprietari, List<Veicolo> veicoli, List<Manutenzione> manutenzioni)> CaricaDatiInizialiAsync()
        {
            var manutenzioni = await Manutenzioni.GetAllManutenzioniAsync();
            var proprietari = await Proprietari.GetAllProprietariAsync();
            var veicoli = await Veicoli.GetVeicoliAsync();
            return (proprietari, veicoli, manutenzioni);
        }
    }
}
