using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VehicleManager.Data;

/// <summary>
/// Factory usata da EF Core Tools (Add-Migration, Update-Database)
/// quando vengono eseguiti dal progetto Data anziché dall'Api.
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=DESKTOP-6DONDJT\\MSSQLSERVER_TRAP;Database=VehicleManager;User Id=sa;Password=admin;Encrypt=False;");
        return new AppDbContext(optionsBuilder.Options);
    }
}