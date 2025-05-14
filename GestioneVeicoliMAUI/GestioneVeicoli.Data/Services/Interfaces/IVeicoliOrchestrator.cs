using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Models;

namespace GestioneVeicoli.Data.Services.Interfaces
{
    public interface IVeicoliOrchestrator
    {
        Task<Veicolo> AddVeicoloAsync(Veicolo veicolo, Proprietario proprietario);
        Task<List<Veicolo>> GetAllVeicoliAsync();
        Task DeleteVeicoloAsync(Veicolo veicolo);

        // Metodi aggiuntivi per Proprietario
        Task<List<Proprietario>> GetAllProprietariAsync();
        Task<Proprietario?> GetProprietarioByIdAsync(int id);
        Task UpdateProprietarioAsync(Proprietario proprietario);
        Task DeleteProprietarioAsync(int id);
    }

    public class VeicoliOrchestrator : IVeicoliOrchestrator
    {
        private readonly IVeicoliRepository _veicoliRepository;
        private readonly IProprietarioRepository _proprietarioRepository;

        public VeicoliOrchestrator(IVeicoliRepository veicoliRepository, IProprietarioRepository proprietarioRepository)
        {
            _veicoliRepository = veicoliRepository;
            _proprietarioRepository = proprietarioRepository;
        }

        public async Task<List<Veicolo>> GetAllVeicoliAsync()
        {
            return await _veicoliRepository.GetVeicoliAsync();
        }

        public async Task<Veicolo> AddVeicoloAsync(Veicolo veicolo, Proprietario proprietario)
        {
            // Controllo se il proprietario esiste
            var proprietarioEsistente = await _proprietarioRepository.GetProprietarioByDetailsAsync(proprietario.Nome, proprietario.Cognome, proprietario.Indirizzo);
            if (proprietarioEsistente != null)
            {
                veicolo.ProprietarioId = proprietarioEsistente.Id;
            }
            else
            {
                // Aggiungi il proprietario se non esiste
                await _proprietarioRepository.AddProprietarioAsync(proprietario);
                veicolo.ProprietarioId = proprietario.Id;
            }

            // Controllo se esiste un veicolo con la stessa targa
            var veicoloEsistente = await _veicoliRepository.GetVeicoloByTargaAsync(veicolo.Targa);
            if (veicoloEsistente != null)
            {
                throw new Exception("Veicolo già esistente con la stessa targa.");
            }

            // Aggiungi il veicolo
            await _veicoliRepository.AddVeicoloAsync(veicolo);
            return veicolo;
        }

        public async Task DeleteVeicoloAsync(Veicolo veicolo)
        {
            await _veicoliRepository.DeleteVeicoloAsync(veicolo.Id);
        }

        public async Task<List<Proprietario>> GetAllProprietariAsync()
        {
            return await _proprietarioRepository.GetAllProprietariAsync();
        }

        public async Task<Proprietario?> GetProprietarioByIdAsync(int id)
        {
            return await _proprietarioRepository.GetByIdAsync(id);
        }

        public async Task UpdateProprietarioAsync(Proprietario proprietario)
        {
            await _proprietarioRepository.UpdateProprietarioAsync(proprietario);
        }

        public async Task DeleteProprietarioAsync(int id)
        {
            await _proprietarioRepository.DeleteProprietarioAsync(id);
        }
    }
}
