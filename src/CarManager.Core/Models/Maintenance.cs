using CarManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Core.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; } = default!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public MaintenanceType MaintenanceType { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public int? Km { get; set; }

        // Consigliato: imposta precision/scale via EF Fluent API (HasPrecision)
        public decimal? Cost { get; set; }

        [MaxLength(150)]
        public string? Workshop { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

    }
}
