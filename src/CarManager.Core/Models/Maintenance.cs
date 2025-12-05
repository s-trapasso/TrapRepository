using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Core.Models
{
    public class Maintenance
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = default!;

        public DateTime Date { get; set; }

        public string Type { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? Km { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Cost { get; set; }

        [MaxLength(150)]
        public string? Workshop { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

    }
}
