using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDesk.Data.Services
{
    public class ProprietarioRepository : IProprietarioRepository
    {
        private readonly CarDeskDbContext _context;

        public ProprietarioRepository(CarDeskDbContext context)
        {
            _context = context;
        }

        public async Task AddProprietarioAsync(Proprietario proprietario)
        {
            _context.Proprietari.Add(proprietario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Proprietario>> GetAllProprietariAsync()
        {
            return await _context.Proprietari.ToListAsync();
        }

        public async Task<Proprietario?> GetByIdAsync(int id)
        {
            return await _context.Proprietari.FindAsync(id);
        }

        public async Task UpdateProprietarioAsync(Proprietario proprietario)
        {
            _context.Proprietari.Update(proprietario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProprietarioAsync(int id)
        {
            var proprietario = await _context.Proprietari.FindAsync(id);
            if (proprietario != null)
            {
                _context.Proprietari.Remove(proprietario);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Proprietario> GetProprietarioByDetailsAsync(string nome, string cognome, string indirizzo)
        {
            return await _context.Proprietari
                .FirstOrDefaultAsync(p => p.Nome == nome && p.Cognome == cognome && p.Indirizzo == indirizzo);
        }
        public async Task<Proprietario> GetProprietarioByNomeCognomeAsync(string nome, string cognome)
        {
            return await _context.Proprietari
                .FirstOrDefaultAsync(p => p.Nome == nome && p.Cognome == cognome);
        }
    }
}
