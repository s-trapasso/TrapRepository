using CarManager.Api.Data;
using CarManager.Api.DTOs;
using CarManager.Api.DTOs.VehicleDTO;
using CarManager.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CarManager.Api.Mappings;

namespace CarManager.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly CarManagerDbContext _db;

        public DashboardService(CarManagerDbContext db)
        {
            _db = db;
        }

        public async Task<DashboardDTO> GetDashboardAsync()
        {
            // 📊 KPI base
            var totalVehicles = await _db.Vehicles.CountAsync();
            var totalOwners = await _db.Owners.CountAsync();
            var totalMaintenances = await _db.Maintenances.CountAsync();

            // 💰 costo totale
            var totalCost = await _db.Maintenances
                .SumAsync(m => m.Cost ?? 0);

            // 🚗 costo manutenzioni per veicolo
            var costByVehicle = await _db.Maintenances
                .Include(m => m.Vehicle)
                .GroupBy(m => new { m.VehicleId, m.Vehicle.Plate })
                .Select(g => new
                {
                    VehicleId = g.Key.VehicleId,
                    Plate = g.Key.Plate,
                    TotalCost = g.Sum(x => x.Cost ?? 0)
                })
                .OrderByDescending(x => x.TotalCost)
                .ToListAsync();
             // 🛠 ultime manutenzioni
             var lastMaintenances = await _db.Maintenances
                 .Include(m => m.Vehicle)
                 .OrderByDescending(m => m.Date)
                 .Take(5)
                 .Select(m => m.ToDto()) 
                 .ToListAsync();

            // 🚗 veicoli senza manutenzione recente (ultimi 6 mesi)
            var recentVehicleIds = await _db.Maintenances
                .Where(m => m.Date >= DateTime.UtcNow.AddMonths(-6))
                .Select(m => m.VehicleId)
                .Distinct()
                .ToListAsync();

            var vehiclesWithoutRecentMaintenance = await _db.Vehicles
                .Where(v => !recentVehicleIds.Contains(v.Id))
                .ToListAsync();

            var vehiclesDto = vehiclesWithoutRecentMaintenance
                .Select(v => new VehicleDTO
                {
                    Id = v.Id,
                    Plate = v.Plate,
                    Brand = v.Brand,
                    Model = v.Model,
                    Year = v.Year,
                    Km = v.Km,
                    FuelType = v.FuelType
                })
                .ToList();

            return new DashboardDTO
            {
                TotalVehicles = totalVehicles,
                TotalOwners = totalOwners,
                TotalMaintenances = totalMaintenances,
                TotalMaintenanceCost = totalCost,
                LastMaintenances = lastMaintenances,
                VehiclesWithoutRecentMaintenance = vehiclesDto
            };
        }
    }
}
