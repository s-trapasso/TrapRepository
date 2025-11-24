using CarManager.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Api.DTOs.VehicleDTO;

public class CreateVehicleDTO
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
