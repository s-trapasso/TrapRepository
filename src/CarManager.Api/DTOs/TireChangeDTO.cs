using CarManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Api.DTOs
{
    public class TireChangeDTO
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public TireType NewTireType { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public int? Km { get; set; }

        public string? Notes { get; set; }
    }
}
