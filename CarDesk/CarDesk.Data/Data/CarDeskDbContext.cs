using CarDesk.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDesk.Data.Data
{
    public class CarDeskDbContext : DbContext
    {
        public CarDeskDbContext(DbContextOptions<CarDeskDbContext> options) : base(options)
        {
        }
        public DbSet<Veicolo> Veicoli { get; set; }
        public DbSet<Proprietario> Proprietari { get; set; }
        //public DbSet<Assicurazione> Assicurazioni { get; set; }
        public DbSet<Manutenzione> Manutenzioni { get; set; }
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
                    .IsRequired();
                    

                entity.Property(v => v.ProprietarioId);


                // Proprietà Alimentazione come enum salvato come stringa
                entity.Property(v => v.Alimentazione)
                    .HasConversion(
                        v => v.ToString(),
                        v => (AlimentazioneEnum)Enum.Parse(typeof(AlimentazioneEnum), v)
                    )
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(v => v.Km)
                    .IsRequired(false);
                // Puoi aggiungere altri indici, vincoli, relazioni qui se necessario
                // Esempio di indice unico sulla Targa:
                entity.HasIndex(v => v.Targa).IsUnique();

            });


            // Configurazione per l'entità Manutenzioni
            modelBuilder.Entity<Manutenzione>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.TipoIntervento)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(m => m.Data)
                    .IsRequired();

                entity.HasOne(m => m.Veicolo)
                    .WithMany(v => v.Manutenzioni)
                    .HasForeignKey(m => m.VeicoloId)
                    .OnDelete(DeleteBehavior.Cascade); // se vuoi cascata
            });

            // Configurazione per l'entità Proprietario
            modelBuilder.Entity<Proprietario>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(p => p.Veicoli)
                    .WithOne(v => v.Proprietario)
                    .HasForeignKey(v => v.ProprietarioId)
                    .OnDelete(DeleteBehavior.Cascade); // oppure Restrict se vuoi impedire la cancellazione
            });

        }
    }
}
