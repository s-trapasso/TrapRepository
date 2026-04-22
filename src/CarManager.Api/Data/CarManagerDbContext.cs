using CarManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

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
        public DbSet<Maintenance> Maintenances => Set<Maintenance>();
    }
}
