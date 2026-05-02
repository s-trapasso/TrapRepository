using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data.Configurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owners");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.Cognome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.CodiceFiscale)
            .HasMaxLength(16);

        builder.Property(o => o.Telefono)
            .HasMaxLength(20);

        builder.Property(o => o.Email)
            .HasMaxLength(200);

        builder.Property(o => o.Indirizzo)
            .HasMaxLength(200);

        builder.Property(o => o.Note)
            .HasMaxLength(500);

        // Ignora la proprietà calcolata NomeCompleto (non va nel DB)
        builder.Ignore(o => o.NomeCompleto);
        builder.Ignore(o => o.StatoPatente);
        builder.Ignore(o => o.StatoVisitaMedica);

        // Relazione con VehicleOwnership
        builder.HasMany(o => o.Veicoli)
            .WithOne(vo => vo.Owner)
            .HasForeignKey(vo => vo.OwnerId)
            .OnDelete(DeleteBehavior.Restrict); // non cancellare owner se ha veicoli
    }
}