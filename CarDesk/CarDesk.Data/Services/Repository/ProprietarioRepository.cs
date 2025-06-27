using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDesk.Data.Services
{
    public class ProprietarioRepository : IProprietarioRepository
    {
        private readonly IDbContextFactory<CarDeskDbContext> _contextFactory;
        private readonly ILoggingService<ProprietarioRepository> _logger;

        public ProprietarioRepository(IDbContextFactory<CarDeskDbContext> contextFactory, ILoggingService<ProprietarioRepository> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }
        /* ---------- CREATE ---------- */
        public async Task AddProprietarioAsync(Proprietario proprietario)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            ctx.Proprietari.Add(proprietario);
            await ctx.SaveChangesAsync();
        }
        /* ---------- READ ---------- */
        public async Task<List<Proprietario>> GetAllProprietariAsync()
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Proprietari.AsNoTracking().ToListAsync();
        }

       
        /* ---------- UPDATE ---------- */
        public async Task UpdateProprietarioAsync(Proprietario proprietario)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            ctx.Proprietari.Update(proprietario);
            await ctx.SaveChangesAsync();
        }
        /* ---------- DELETE ---------- */
        public async Task DeleteProprietarioAsync(int id)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            var proprietario = await ctx.Proprietari.FindAsync(id);
            if (proprietario != null)
            {
                ctx.Proprietari.Remove(proprietario);
                await ctx.SaveChangesAsync();
            }
        }
        public async Task<Proprietario?> GetByIdAsync(int id)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Proprietari.FindAsync(id);
        }
        public async Task<Proprietario?> GetProprietarioByDetailsAsync(string nome, string cognome, string indirizzo)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Proprietari.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Nome == nome && p.Cognome == cognome && p.Indirizzo == indirizzo);
        }
        public async Task<Proprietario?> GetProprietarioByNomeCognomeAsync(string nome, string cognome)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Proprietari.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Nome == nome && p.Cognome == cognome);
        }
    }
}
