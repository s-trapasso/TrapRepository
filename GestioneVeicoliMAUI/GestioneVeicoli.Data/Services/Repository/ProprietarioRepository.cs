using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Data;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestioneVeicoli.Data.Services.VeicoloRepository
{
    public class ProprietarioRepository : IProprietarioRepository
    {
        private readonly VeicoliDbContext _context;

        public ProprietarioRepository(VeicoliDbContext context)
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
    }
}
