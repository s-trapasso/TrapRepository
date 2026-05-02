using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Configurations;

public class VehicleOwnershipConfiguration : IEntityTypeConfiguration<VehicleOwnership>
{
    public void Configure(EntityTypeBuilder<VehicleOwnership> builder)
    {
        builder.ToTable("VehicleOwnerships");

        builder.HasKey(vo => vo.Id);

        builder.Property(vo => vo.TipoProprietà)
            .HasConversion<int>();

        builder.Property(vo => vo.PrezzoAcquisto)
            .HasColumnType("decimal(10,2)");

        builder.Property(vo => vo.PrezzoVendita)
            .HasColumnType("decimal(10,2)");

        builder.Property(vo => vo.Note)
            .HasMaxLength(500);

        // Ignora proprietà calcolate
        builder.Ignore(vo => vo.IsAttuale);

        // Indice per trovare rapidamente il proprietario attuale di un veicolo
        builder.HasIndex(vo => new { vo.VehicleId, vo.DataCessione });
    }
}