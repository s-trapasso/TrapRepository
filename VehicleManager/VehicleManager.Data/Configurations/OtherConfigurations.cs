using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Configurations;

public class DeadlineConfiguration : IEntityTypeConfiguration<Deadline>
{
    public void Configure(EntityTypeBuilder<Deadline> builder)
    {
        builder.ToTable("Deadlines");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Tipo)
            .HasConversion<int>();

        builder.Property(d => d.Descrizione)
            .HasMaxLength(200);

        builder.Property(d => d.Costo)
            .HasColumnType("decimal(10,2)");

        builder.Property(d => d.Compagnia)
            .HasMaxLength(200);

        builder.Property(d => d.NumeroPolizza)
            .HasMaxLength(100);

        builder.Property(d => d.Note)
            .HasMaxLength(500);

        // Ignora le proprietà calcolate: non vanno nel DB
        builder.Ignore(d => d.GiorniAllaScadenza);
        builder.Ignore(d => d.Stato);

        // Indice per il recupero scadenze per veicolo e data
        builder.HasIndex(d => new { d.VehicleId, d.DataScadenza });
    }
}

public class VehicleDocumentConfiguration : IEntityTypeConfiguration<VehicleDocument>
{
    public void Configure(EntityTypeBuilder<VehicleDocument> builder)
    {
        builder.ToTable("VehicleDocuments");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.NomeFile)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.PathFile)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(d => d.MimeType)
            .HasMaxLength(100);

        builder.Property(d => d.Descrizione)
            .HasMaxLength(200);

        // Indice per recuperare documenti/foto di un veicolo
        builder.HasIndex(d => new { d.VehicleId, d.IsFoto });
    }
}