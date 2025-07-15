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
        public DbSet<VoceIntervento> VoceIntervento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurazione per l'entità Veicolo
            modelBuilder.Entity<Veicolo>(entity =>
            {
                // Chiave primaria
                entity.HasKey(v => v.Id);

                // Proprietà Targa
                entity.Property(v => v.Targa).IsRequired().HasMaxLength(50);

                // Proprietà Marca
                entity.Property(v => v.Marca).IsRequired().HasMaxLength(50);

                // Proprietà Modello
                entity.Property(v => v.Modello).IsRequired().HasMaxLength(50);

                // Proprietà Anno
                entity.Property(v => v.Anno).IsRequired();

                //Collegamento con proprietario
                entity.HasOne(v => v.Proprietario)
                      .WithMany(p => p.Veicoli)
                      .HasForeignKey(v => v.ProprietarioId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Proprietà Alimentazione come enum salvato come stringa
                entity.Property(v => v.Alimentazione)
                    .HasConversion(
                        v => v.ToString(),
                        v => (AlimentazioneEnum)Enum.Parse(typeof(AlimentazioneEnum), v, true)
                    )
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(v => v.Km).IsRequired(false);

                //Vincolo di Unicità sulla Targa, per evitare duplicati
                entity.HasIndex(v => v.Targa).IsUnique();
            });


            // Configurazione per l'entità Manutenzioni
            modelBuilder.Entity<Manutenzione>(entity =>
            {
                // Chiave primaria
                entity.HasKey(m => m.Id);
                
                // Proprietà Data
                entity.Property(m => m.Data).IsRequired();

                // Proprietà Costo
                entity.Property(m => m.Costo).HasColumnType("decimal(10,2)").IsRequired();

                // Configurazione relazione 1 a 1 con Veicolo
                entity.HasOne(m => m.Veicolo)
                    .WithMany(v => v.Manutenzioni)
                    .HasForeignKey(m => m.VeicoloId)
                    .OnDelete(DeleteBehavior.Cascade); // se vuoi cascata
               
                // Configurazione relazione 1 a molti con VociIntervento
                entity.HasMany(m => m.VociIntervento)
                      .WithOne(v => v.Manutenzione)
                      .HasForeignKey(v => v.ManutenzioneId)
                      .OnDelete(DeleteBehavior.Cascade); // se vuoi che eliminando manutenzione si eliminino le voci
            });

            // Configurazione per l'entità Proprietario
            modelBuilder.Entity<Proprietario>(entity =>
            {
                // Chiave primaria
                entity.HasKey(p => p.Id);

                // Proprietà Nome
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);

                // Proprietà Cognome
                entity.Property(p => p.Cognome).IsRequired().HasMaxLength(100);

                // Proprietà Indirizzo
                entity.Property(p => p.Indirizzo).IsRequired().HasMaxLength(100);

                ////Proprietà DataNascita
                //entity.Property(p => p.DataNascita).IsRequired();

                //// Proprietà LuogoNascita
                //entity.Property(p => p.LuogoNascita).IsRequired().HasMaxLength(100);

                //// Proprietà Sesso
                //entity.Property(p => p.Sesso).IsRequired().HasMaxLength(1);

                //// Proprietà CodiceFiscale
                //entity.Property(p => p.CodiceFiscale).IsRequired().HasMaxLength(16);

                //// Vincolo di Unicità sul Codice Fiscale
                //entity.HasMany(p => p.Veicoli).WithOne(v => v.Proprietario)
                //      .HasForeignKey(v => v.ProprietarioId).OnDelete(DeleteBehavior.Cascade); // oppure Restrict se vuoi impedire la cancellazione

            });
            // Configurazione per l'entità VociIntervento
            modelBuilder.Entity<VoceIntervento>(entity =>
            {
                // Chiave primaria
                entity.HasKey(m => m.Id);

                // Proprietà Descrizione
                entity.Property(m => m.Descrizione).IsRequired();

                // Proprietà Costo
                entity.Property(m => m.Costo).HasColumnType("decimal(10,2)").IsRequired();

                // Configurazione relazione 1 a molti con Manutenzione
                entity.HasOne(v => v.Manutenzione)
                    .WithMany(m => m.VociIntervento)
                    .HasForeignKey(v => v.ManutenzioneId)
                    .OnDelete(DeleteBehavior.Cascade); // se vuoi che eliminando manutenzione si eliminino le voci
            });
        }
    }
}
