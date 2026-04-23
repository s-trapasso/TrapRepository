using CarManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Api.DTOs.Vehicle;

public class UpdateVehicleDTO
{
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

    public FuelTypeEnum FuelType { get; set; }
}