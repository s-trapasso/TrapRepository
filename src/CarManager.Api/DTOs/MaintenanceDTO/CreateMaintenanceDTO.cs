using CarManager.Core.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManager.Api.DTOs.MaintenanceDTO
{
    public class CreateMaintenanceDTO
    {
        [Required(ErrorMessage = "Veicolo obbligatorio")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "La data è obbligatoria")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Il tipo è obbligatorio")]
        public MaintenanceType MaintenanceType { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Range(0, 9999999)]
        public int? Km { get; set; }

        [Range(0, 999999.99)]
        public decimal? Cost { get; set; }

        [MaxLength(150)]
        public string? Workshop { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
