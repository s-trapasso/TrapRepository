using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Models.Maintenance
{
    public class MaintenanceCreateModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Veicolo obbligatorio")]
        public int VehicleId { get; set; }
        public string? VehiclePlate { get; set; }

        [Required(ErrorMessage = "La data è obbligatoria")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Il tipo è obbligatorio")]
        [MaxLength(100)]
        public string Type { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Range(0, 9999999, ErrorMessage = "I km devono essere validi")]
        public int? Km { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, 999999.99, ErrorMessage = "Costo non valido")]
        public decimal? Cost { get; set; }

        [MaxLength(150)]
        public string? Workshop { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
