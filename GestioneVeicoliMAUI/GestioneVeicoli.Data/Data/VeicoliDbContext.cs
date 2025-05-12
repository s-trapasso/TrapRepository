using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneVeicoli.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GestioneVeicoli.Data.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurazione per l'entità Veicolo
            modelBuilder.Entity<Veicolo>(entity =>
            {
                // Chiave primaria
                entity.HasKey(v => v.Id);

                // Proprietà Targa
                entity.Property(v => v.Targa)
                    .IsRequired()
                    .HasMaxLength(50);

                // Proprietà Marca
                entity.Property(v => v.Marca)
                    .IsRequired()
                    .HasMaxLength(50);

                // Proprietà Modello
                entity.Property(v => v.Modello)
                    .IsRequired()
                    .HasMaxLength(50);

                // Proprietà Anno
                entity.Property(v => v.Anno)
                    .IsRequired()
                    .HasMaxLength(4); // Anche se è un int, in database potrebbe essere varchar(4)

                // Puoi aggiungere altri indici, vincoli, relazioni qui se necessario
                // Esempio di indice unico sulla Targa:
                entity.HasIndex(v => v.Targa).IsUnique();
            });

            // Se hai altre entità, configura anche quelle qui sotto
            // modelBuilder.Entity<AltraEntita>(entity => { ... });
        }
    }
}
