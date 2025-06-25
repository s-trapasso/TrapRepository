using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarDesk.Data.Services
{
    public class VeicoliRepository : IVeicoliRepository
    {
        private readonly CarDeskDbContext _context;
        private readonly ILoggingService<VeicoliRepository> _logger;

        public VeicoliRepository(CarDeskDbContext context, ILoggingService<VeicoliRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // CREATE
        public async Task AddVeicoloAsync(Veicolo veicolo)
        {
            _logger.LogInformation("Inserimento nuovo veicolo: {Targa}", veicolo.Targa);
            try
            {
                await _context.Veicoli.AddAsync(veicolo);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Errore durante l'inserimento del veicolo: {Targa}", veicolo.Targa);
                throw; // Rilancia l'eccezione per gestirla a livello superiore
            }
           
        }

        // READ
        public async Task<List<Veicolo>> GetVeicoliAsync()
        {
            return await _context.Veicoli
                .Include(v => v.Proprietario)
                .ToListAsync();
        }

        // UPDATE
        public async Task UpdateVeicoloAsync(Veicolo veicolo)
        {
            _context.Veicoli.Update(veicolo);
            await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteVeicoloAsync(int id)
        {
            var veicolo = await _context.Veicoli.FindAsync(id);
            if (veicolo != null)
            {
                _context.Veicoli.Remove(veicolo);
                await _context.SaveChangesAsync();
            }
        }
        //Get Veicolo by Targa
        public async Task<Veicolo> GetVeicoloByTargaAsync(string targa)
        {
            return await _context.Veicoli
                .FirstOrDefaultAsync(v => v.Targa == targa);
        }
    }

}
