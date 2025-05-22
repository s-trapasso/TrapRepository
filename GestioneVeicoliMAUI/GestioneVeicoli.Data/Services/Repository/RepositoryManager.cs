using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Data;
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
    }
}
