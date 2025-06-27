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
        
        private readonly IDbContextFactory<CarDeskDbContext> _contextFactory;
        private readonly ILoggingService<ManutenzioneRepository> _logger;
        public ManutenzioneRepository(IDbContextFactory<CarDeskDbContext> contextFactory, ILoggingService<ManutenzioneRepository> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        /* ---------- CREATE ---------- */
        public async Task AddAsync(Manutenzione manutenzione)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            await ctx.Manutenzioni.AddAsync(manutenzione);
            await ctx.SaveChangesAsync();
        }

        /* ---------- READ ---------- */
        public async Task<List<Manutenzione>> GetAllManutenzioniAsync()
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Manutenzioni
                 .OrderByDescending(m => m.Data)
                 .AsNoTracking()
                 .ToListAsync();
        }

        /* ---------- UPDATE ---------- */
        public async Task UpdateAsync(Manutenzione manutenzione)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            ctx.Manutenzioni.Update(manutenzione);
            await ctx.SaveChangesAsync();
        }

        /* ---------- DELETE ---------- */
        public async Task DeleteAsync(int id)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            var entity = await ctx.Manutenzioni.FindAsync(id);
            if (entity != null)
            {
                ctx.Manutenzioni.Remove(entity);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<Manutenzione>> GetByVeicoloIdAsync(int veicoloId)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Manutenzioni
                .Where(m => m.VeicoloId == veicoloId)
                .OrderByDescending(m => m.Data)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
