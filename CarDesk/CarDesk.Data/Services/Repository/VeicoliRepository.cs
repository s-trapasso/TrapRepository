using CarDesk.Data.Data;
using CarDesk.Data.Models;
using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarDesk.Data.Services
{
    public class VeicoliRepository : IVeicoliRepository
    {
        private readonly IDbContextFactory<CarDeskDbContext> _contextFactory;
        private readonly ILoggingService<VeicoliRepository> _logger;

        public VeicoliRepository(IDbContextFactory<CarDeskDbContext> contextFactory, ILoggingService<VeicoliRepository> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        /* ---------- CREATE ---------- */
        public async Task AddVeicoloAsync(Veicolo veicolo)
        {
            _logger.LogInformation("Inserimento nuovo veicolo: {Targa}", veicolo.Targa);
            try
            {
                await using var ctx = _contextFactory.CreateDbContext();
                await ctx.Veicoli.AddAsync(veicolo);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Errore durante l'inserimento del veicolo: {Targa}", veicolo.Targa);
                throw; // Rilancia l'eccezione per gestirla a livello superiore
            }
           
        }

        /* ---------- READ ---------- */
        public async Task<List<Veicolo>> GetVeicoliAsync()
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Veicoli
                .Include(v => v.Proprietario)
                .AsNoTracking()
                .ToListAsync();
        }

        /* ---------- UPDATE ---------- */
        public async Task UpdateVeicoloAsync(Veicolo veicolo)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            _logger.LogInformation("Salvataggio veicolo: {Targa}", veicolo.Targa);
            // se esiste già un'istanza tracciata con la stessa chiave, sganciala
            var local = ctx.Veicoli.Local.FirstOrDefault(v => v.Id == veicolo.Id);
            if (local is not null)
                ctx.Entry(local).State = EntityState.Detached;
            ctx.Veicoli.Update(veicolo);
            await ctx.SaveChangesAsync();
        }

        /* ---------- DELETE ---------- */
        public async Task DeleteVeicoloAsync(int id)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            var veicolo = await ctx.Veicoli.FindAsync(id);
            if (veicolo != null)
            {
                ctx.Veicoli.Remove(veicolo);
                await ctx.SaveChangesAsync();
            }
        }
        /*----------- Get Veicolo by Targa --------*/
        public async Task<Veicolo?> GetVeicoloByTargaAsync(string targa)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Veicoli
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Targa == targa);
        }

        /*----------- Get Veicolo by Id -----------*/
         public async Task<Veicolo?> GetVeicoloByIdAsync(int? id)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Veicoli
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }
    }

}
