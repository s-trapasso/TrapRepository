using Microsoft.EntityFrameworkCore;
using VehicleManager.Shared.Entities;

namespace VehicleManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ── DbSet ─────────────────────────────────────────────────────────────────
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<VehicleOwnership> VehicleOwnerships => Set<VehicleOwnership>();
    public DbSet<Maintenance> Maintenances => Set<Maintenance>();
    public DbSet<Deadline> Deadlines => Set<Deadline>();
    public DbSet<VehicleDocument> VehicleDocuments => Set<VehicleDocument>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applica tutte le configurazioni Fluent API dalla cartella Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    // Aggiorna automaticamente UpdatedAt prima di ogni salvataggio
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<Shared.Entities.BaseEntity>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
            entry.Entity.UpdatedAt = DateTime.UtcNow;
    }
}