using CarManager.Core.Enums;
using CarManager.Core.Extensions;
using CarManager.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManager.Api.DTOs.MaintenanceDTO
{
    public class MaintenanceDTO
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public string? VehiclePlate { get; set; }

        public DateTime Date { get; set; }

        public MaintenanceType MaintenanceType { get; set; }
        public string MaintenanceTypeName => MaintenanceType.GetDescription();
        public string? Description { get; set; }

        public int? Km { get; set; }

        public decimal? Cost { get; set; }

        public string? Workshop { get; set; }

        public string? Notes { get; set; }
    }
}
