using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Data;
using GestioneVeicoli.Data.Models;
using GestioneVeicoli.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestioneVeicoli.Data.Services.Repository
{
    public class ManutenzioneRepository : IManutenzioneRepository
    {
        private readonly VeicoliDbContext _context;

        public ManutenzioneRepository(VeicoliDbContext context)
        {
            _context = context;
        }

        public async Task<List<Manutenzione>> GetByVeicoloIdAsync(int veicoloId)
        {
            //using var _context = new VeicoliDb_context(_connectionString);
            return await _context.Manutenzioni
                .Where(m => m.VeicoloId == veicoloId)
                .OrderByDescending(m => m.Data)
                .ToListAsync();
        }

        public async Task AddAsync(Manutenzione manutenzione)
        {
            //using var _context = new VeicoliDb_context(_connectionString);
            await _context.Manutenzioni.AddAsync(manutenzione);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Manutenzione manutenzione)
        {
            //using var _context = new VeicoliDb_context(_connectionString);
            _context.Manutenzioni.Update(manutenzione);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            //using var _context = new VeicoliDb_context(_connectionString);
            var entity = await _context.Manutenzioni.FindAsync(id);
            if (entity != null)
            {
                _context.Manutenzioni.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
