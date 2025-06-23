using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarDesk.Data.Data
{
    public class CarDeskDbContextFactory : IDesignTimeDbContextFactory<CarDeskDbContext>
    {
        public CarDeskDbContext CreateDbContext(string[] args)
        {

            //1. Carica le configurazioni dal file appSettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 2. Recupera la stringa di connessione
            var connectionString = configuration.GetConnectionString("CarDeskDBDev");

            // 3. Configura il DbContext
            var optionsBuilder = new DbContextOptionsBuilder<CarDeskDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CarDeskDbContext(optionsBuilder.Options);
        }
    }
}
