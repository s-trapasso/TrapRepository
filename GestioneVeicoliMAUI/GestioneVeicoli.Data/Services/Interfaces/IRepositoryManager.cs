using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Models;

namespace GestioneVeicoli.Data.Services.Interfaces
{
    public interface IRepositoryManager
    {
        IVeicoliRepository Veicoli { get; }
        IProprietarioRepository Proprietari { get; }

        IManutenzioneRepository Manutenzioni { get; }
        // Aggiungeremo Manutenzioni, Scadenze, ecc. in seguito
        Task<(List<Proprietario> proprietari, List<Veicolo> veicoli, List<Manutenzione> manutenzioni)> CaricaDatiInizialiAsync();
    }
}
