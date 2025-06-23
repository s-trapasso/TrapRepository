using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDesk.Data.Services.Repository
{
    public class ManutenzioneRepository : IManutenzioneRepository
    {
        private readonly CarDeskDbContext _context;

        public ManutenzioneRepository(CarDeskDbContext context)
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

        public async Task<List<Manutenzione>> GetAllManutenzioniAsync()
        {
            return await _context.Manutenzioni
                 .OrderByDescending(m => m.Data)
                 .ToListAsync();
        }
    }
}
