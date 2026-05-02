using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Targa)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(v => v.Marca)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Modello)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Versione)
            .HasMaxLength(50);

        builder.Property(v => v.Colore)
            .HasMaxLength(30);

        builder.Property(v => v.Vin)
            .HasMaxLength(17);

        builder.Property(v => v.Note)
            .HasMaxLength(1000);

        builder.Property(v => v.PrezzoAcquisto)
            .HasColumnType("decimal(10,2)");

        builder.Property(v => v.Tipo)
            .HasConversion<int>();

        builder.Property(v => v.Carburante)
            .HasConversion<int>();

        // Indice univoco sulla targa
        builder.HasIndex(v => v.Targa)
            .IsUnique();

        // Relazioni
        builder.HasMany(v => v.Manutenzioni)
            .WithOne(m => m.Vehicle)
            .HasForeignKey(m => m.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Proprietari)
            .WithOne(o => o.Vehicle)
            .HasForeignKey(o => o.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Scadenze)
            .WithOne(d => d.Vehicle)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Documenti)
            .WithOne(d => d.Vehicle)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}