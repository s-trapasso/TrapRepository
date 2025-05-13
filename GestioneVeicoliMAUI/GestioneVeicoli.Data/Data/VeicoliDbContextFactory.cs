using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GestioneVeicoli.Data.Data
{
    public class VeicoliDbContextFactory : IDesignTimeDbContextFactory<VeicoliDbContext>
    {
        public VeicoliDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VeicoliDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new VeicoliDbContext(optionsBuilder.Options);
        }
    }
}
