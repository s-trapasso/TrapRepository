using CarManager.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Models.Vehicle
{
    public class VehicleCreateModel
    {
        [Required]
        [StringLength(10)]
        public string Plate { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = default!;

        [Range(1900, 2100)]
        public int Year { get; set; }
        [Range(0, int.MaxValue)]
        public int Km { get; set; }
        [Required]
        public FuelTypeEnum FuelType { get; set; }
    }
}
