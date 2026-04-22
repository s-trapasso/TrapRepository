using CarManager.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Core.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = default!;

        public DateTime Date { get; set; }

        public MaintenanceType MaintenanceType { get; set; }

        public string? Description { get; set; }

        public int? Km { get; set; }

        public decimal? Cost { get; set; }

        [MaxLength(150)]
        public string? Workshop { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

    }
}
