using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Configurations;

public class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
{
    public void Configure(EntityTypeBuilder<Maintenance> builder)
    {
        builder.ToTable("Maintenances");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Tipo)
            .HasConversion<int>();

        builder.Property(m => m.Descrizione)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Costo)
            .HasColumnType("decimal(10,2)");

        builder.Property(m => m.Officina)
            .HasMaxLength(200);

        builder.Property(m => m.NumeroFattura)
            .HasMaxLength(50);

        builder.Property(m => m.Note)
            .HasMaxLength(1000);

        // Indice per recuperare rapidamente lo storico di un veicolo
        builder.HasIndex(m => new { m.VehicleId, m.DataIntervento });

        // Relazione con VehicleDocument
        builder.HasMany(m => m.Documenti)
            .WithOne(d => d.Maintenance)
            .HasForeignKey(d => d.MaintenanceId)
            .OnDelete(DeleteBehavior.NoAction); // se si cancella la manutenzione, il documento rimane sul veicolo
    }
}