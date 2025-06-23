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

            var optionsBuilder = new DbContextOptionsBuilder<CarDeskDbContext>();

            // Sostituisci la connection string con quella reale
            optionsBuilder.UseSqlServer("Server=DESKTOP-6DONDJT\\MSSQLSERVER_TRAP;Database=CarDesk;User Id=sa;Password=admintrap;Encrypt=False;");

            return new CarDeskDbContext(optionsBuilder.Options);
        }
    }
}
