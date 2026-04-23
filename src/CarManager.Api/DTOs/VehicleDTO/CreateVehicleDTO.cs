using CarManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Api.DTOs.VehicleDTO;

public class CreateVehicleDTO
{
    [Required]
    [StringLength(10, ErrorMessage = "La targa può avere massimo 10 caratteri")]
    public string Plate { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Model { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int Year { get; set; }

    [Range(0, int.MaxValue)]
    public int Km { get; set; }

    [Required]
    public FuelTypeEnum FuelType { get; set; }
}