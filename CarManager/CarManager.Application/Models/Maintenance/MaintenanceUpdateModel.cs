using CarManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Application.Models.Maintenance
{
    public class MaintenanceUpdateModel
    {
        public int VehicleId { get; set; }
        public string? VehiclePlate { get; set; }
        public DateTime Date { get; set; }
        public MaintenanceType MaintenanceType { get; set; } // enum (int)
        public string? Description { get; set; }
        public int? Km { get; set; }
        public decimal? Cost { get; set; }
        public string? Workshop { get; set; }
        public string? Notes { get; set; }
    }
}
