using CarManager.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Api.DTOs.VehicleDTO
{
    public class VehicleDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(50)]
        public string Plate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(50)]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obbligatorio")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public FuelTypeEnum FuelType { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int Km { get; set; }

    }
}
