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
    public class VeicoliRepository : IVeicoliRepository
    {
        private readonly VeicoliDbContext _context;

        public VeicoliRepository(VeicoliDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddVeicoloAsync(Veicolo veicolo)
        {
            await _context.Veicoli.AddAsync(veicolo);
            await _context.SaveChangesAsync();
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
