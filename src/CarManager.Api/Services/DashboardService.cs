using CarManager.Api.Data;
using CarManager.Api.DTOs;
using CarManager.Api.DTOs.Maintenance;
using CarManager.Api.DTOs.Vehicle;
using CarManager.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly CarManagerDbContext _db;

        public DashboardService(CarManagerDbContext db)
        {
            _db = db;
        }

        public async Task<DashboardDTO> GetDashboardAsync(DashboardQuery query)
        {
            var vehiclesQuery = _db.Vehicles.AsQueryable();
            var maintQuery = _db.Maintenances.AsQueryable();

            if (query.VehicleId.HasValue)
                maintQuery = maintQuery.Where(m => m.VehicleId == query.VehicleId);

            if (query.OwnerId.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.OwnerId == query.OwnerId);

            var limitDate = DateTime.Now.AddMonths(-query.MaintenanceMonthsBack);

            // ✅ SERIALIZZATO (NO PARALLEL EF)
            var totalVehicles = await vehiclesQuery.CountAsync();
            var totalOwners = await _db.Owners.CountAsync();
            var totalMaint = await maintQuery.CountAsync();

            var totalCost = await maintQuery.SumAsync(x => x.Cost ?? 0);

            var lastMaint = await maintQuery
                .OrderByDescending(m => m.Date)
                .Take(5)
                .Select(m => new MaintenanceDTO
                {
                    Id = m.Id,
                    VehicleId = m.VehicleId,
                    Date = m.Date,
                    Cost = m.Cost,
                    MaintenanceType = m.MaintenanceType
                })
                .ToListAsync();

            var vehiclesNoMaint = await vehiclesQuery
                .Where(v => !maintQuery.Any(m =>
                    m.VehicleId == v.Id &&
                    m.Date >= limitDate))
                .Select(v => new VehicleDTO
                {
                    Id = v.Id,
                    Plate = v.Plate,
                    Brand = v.Brand,
                    Model = v.Model,
                    Km = v.Km
                })
                .ToListAsync();

            var maintenanceByType = await maintQuery
                .GroupBy(m => m.MaintenanceType)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Key.ToString(), x => x.Count);

            var fuelByType = await vehiclesQuery
                .GroupBy(v => v.FuelType)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Key.ToString(), x => x.Count);

            var alerts = new List<AlertDTO>();

            if (totalMaint == 0)
            {
                alerts.Add(new AlertDTO
                {
                    Code = "NO_MAINTENANCE",
                    Message = "Nessuna manutenzione registrata",
                    Severity = "Warning"
                });
            }

            var highKmVehicles = await vehiclesQuery
                .Where(v => v.Km > 150000)
                .Select(v => new AlertDTO
                {
                    Code = "HIGH_KM",
                    Message = "Veicolo con chilometraggio elevato",
                    Severity = "Warning",
                    VehicleId = v.Id,
                    Plate = v.Plate
                })
                .ToListAsync();

            alerts.AddRange(highKmVehicles);

            return new DashboardDTO
            {
                TotalVehicles = totalVehicles,
                TotalOwners = totalOwners,
                TotalMaintenances = totalMaint,

                TotalMaintenanceCost = totalCost,
                AvgCostPerVehicle = totalVehicles == 0 ? 0 : totalCost / totalVehicles,

                LastMaintenances = lastMaint,
                VehiclesWithoutRecentMaintenance = vehiclesNoMaint,

                MaintenanceByType = maintenanceByType,
                VehiclesByFuelType = fuelByType,

                Alerts = alerts
            };
        }
    }
}
