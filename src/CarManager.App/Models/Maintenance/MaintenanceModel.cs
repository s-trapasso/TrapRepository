using CarManager.App.Models.Vehicle;
using CarManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Models.Maintenance
{
    public class MaintenanceModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string? VehiclePlate { get; set; }
        public DateTime Date { get; set; }

        public MaintenanceType MaintenanceType { get; set; }
        public string? MaintenanceTypeName { get; set; }

        public string? Description { get; set; }
        public int? Km { get; set; }
        public decimal? Cost { get; set; }
        public string? Workshop { get; set; }
        public string? Notes { get; set; }
    }
}
