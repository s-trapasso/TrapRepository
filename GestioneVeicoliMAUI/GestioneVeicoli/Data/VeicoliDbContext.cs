using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Models;
using Microsoft.EntityFrameworkCore;

namespace GestioneVeicoli.Data
{
    public class VeicoliDbContext: DbContext
    {
        public VeicoliDbContext(DbContextOptions<VeicoliDbContext> options) : base(options)
        {
        }
        public DbSet<Veicolo> Veicoli { get; set; }
        //public DbSet<Proprietario> Proprietari { get; set; }
        //public DbSet<Assicurazione> Assicurazioni { get; set; }
        //public DbSet<Manutenzione> Manutenzioni { get; set; }
        //public DbSet<Officina> Officine { get; set; }
        //public DbSet<Intervento> Interventi { get; set; }
    }
}
