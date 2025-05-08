using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data;
using GestioneVeicoli.Models;
using Microsoft.EntityFrameworkCore;

namespace GestioneVeicoli.Services.VeicoloRepository
{
    public class VeicoliRepository : IVeicoliRepository
    {
        //private readonly List<Veicolo> _veicoli = new()
        //{
        //    new Veicolo { Id = 1, Targa = "AB123CD", Marca = "Fiat", Modello = "Panda", Anno = 2015 },
        //    new Veicolo { Id = 2, Targa = "EF456GH", Marca = "Toyota", Modello = "Yaris", Anno = 2018 }
        //};

        //public Task<List<Veicolo>> GetVeicoliAsync() => Task.FromResult(_veicoli);

        //public Task AddVeicoloAsync(Veicolo veicolo)
        //{
        //    veicolo.Id = _veicoli.Count + 1;
        //    _veicoli.Add(veicolo);
        //    return Task.CompletedTask;
        //}

        //public Task UpdateVeicoloAsync(Veicolo veicolo)
        //{
        //    var index = _veicoli.FindIndex(v => v.Id == veicolo.Id);
        //    if (index >= 0)
        //        _veicoli[index] = veicolo;
        //    return Task.CompletedTask;
        //}

        //public Task DeleteVeicoloAsync(int id)
        //{
        //    var veicolo = _veicoli.FirstOrDefault(v => v.Id == id);
        //    if (veicolo != null)
        //        _veicoli.Remove(veicolo);
        //    return Task.CompletedTask;
        //}
        //public async Task<Veicolo> GetVeicoloByIdAsync(int? id)
        //{
        //    // Verifica se l'ID è null
        //    if (id == null)
        //    {
        //        // Se l'ID è null, ritorna null o un comportamento desiderato
        //        return null;
        //    }

        //    // Simula una chiamata asincrona (ad esempio, un'operazione di I/O)
        //    await Task.Delay(500); // Simulazione di latenza

        //    // Trova il veicolo in base all'ID
        //    return _veicoli.FirstOrDefault(v => v.Id == id.Value);
        //}

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
            return await _context.Veicoli.ToListAsync();
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
    }

}
