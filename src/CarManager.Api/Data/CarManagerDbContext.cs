using CarManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Data
{
    public class CarManagerDbContext : DbContext
    {
        public CarManagerDbContext(DbContextOptions<CarManagerDbContext> options)
        : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Owner> Owners => Set<Owner>();
    }
}
