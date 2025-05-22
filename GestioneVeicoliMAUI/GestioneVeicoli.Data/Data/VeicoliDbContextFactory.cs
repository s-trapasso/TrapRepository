using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace GestioneVeicoli.Data.Data
{
    public class VeicoliDbContextFactory : IDesignTimeDbContextFactory<VeicoliDbContext>
    {
        public VeicoliDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VeicoliDbContext>();

            // Sostituisci la connection string con quella reale
            optionsBuilder.UseSqlServer("Server=DESKTOP-6DONDJT\\MSSQLSERVER_TRAP;Database=GestioneVeicoli;User Id=sa;Password=admintrap;Encrypt=False;");

            return new VeicoliDbContext(optionsBuilder.Options);
        }
    }
}
