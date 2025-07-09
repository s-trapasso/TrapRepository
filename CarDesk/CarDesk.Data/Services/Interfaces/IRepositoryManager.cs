using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.DTOs;
using CarDesk.Data.Models;

namespace CarDesk.Data.Services.Interfaces
{
    public interface IRepositoryManager 
    {
        //IVeicoliRepository Veicoli { get; }
        //IProprietarioRepository Proprietari { get; }

        //IManutenzioneRepository Manutenzioni { get; }
        //// Aggiungeremo Manutenzioni, Scadenze, ecc. in seguito
        //Task<(List<Veicolo> veicoli, List<Proprietario> proprietari, List<Manutenzione> manutenzioni)> CaricaDatiInizialiAsync();
        IGenericRepository<T> For<T>() where T : class;
        Task<DatiInizialiDto> CaricaDatiInizialiAsync();
    }
}
