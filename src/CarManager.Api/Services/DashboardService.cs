using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;
        public DashboardService(CarManagerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<DashboardDTO> GetDashboardAsync(DashboardQuery query)
        {
            var vehiclesQuery = _db.Vehicles.AsQueryable();
            var maintQuery = _db.Maintenances.AsQueryable();

            // =========================
            // FILTRI
            // =========================
            if (query.VehicleId.HasValue)
                maintQuery = maintQuery.Where(m => m.VehicleId == query.VehicleId.Value);

            if (query.OwnerId.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.OwnerId == query.OwnerId.Value);

            var limitDate = DateTime.Now.AddMonths(-query.MaintenanceMonthsBack);

            // =========================
            // METRICHE BASE
            // =========================
            var totalVehicles = await vehiclesQuery.CountAsync();
            var totalOwners = await _db.Owners.AsNoTracking().CountAsync();
            var totalMaint = await maintQuery.CountAsync();
            var totalCost = await maintQuery.SumAsync(x => x.Cost ?? 0);

            // =========================
            // ULTIME MANUTENZIONI
            // =========================
            var lastMaint = await maintQuery
                .OrderByDescending(m => m.Date)
                .Take(5)
                .ProjectTo<MaintenanceDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // =========================
            // VEICOLI SENZA MANUTENZIONE RECENTE
            // =========================
            var maintForVehicleCheck = _db.Maintenances.AsNoTracking().AsQueryable();

            if (query.OwnerId.HasValue)
            {
                maintForVehicleCheck = maintForVehicleCheck.Where(m =>
                    vehiclesQuery.Select(v => v.Id).Contains(m.VehicleId));
            }

            var vehiclesNoMaint = await vehiclesQuery
                .Where(v => !maintForVehicleCheck.Any(m =>
                    m.VehicleId == v.Id &&
                    m.Date >= limitDate))
                .ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // =========================
            // STATISTICHE MANUTENZIONI
            // =========================
            var maintenanceByType = await maintQuery
                .GroupBy(m => m.MaintenanceType)
                .Select(g => new
                {
                    Key = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToDictionaryAsync(x => x.Key, x => x.Count);

            // =========================
            // STATISTICHE CARBURANTE
            // =========================
            var fuelByType = await vehiclesQuery
                .GroupBy(v => v.FuelType)
                .Select(g => new
                {
                    Key = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToDictionaryAsync(x => x.Key, x => x.Count);

            // =========================
            // ALERTS
            // =========================
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
                .ProjectTo<VehicleDTO>(_mapper.ConfigurationProvider)
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

            // =========================
            // RESULT FINALE
            // =========================
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
