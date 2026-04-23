using CarManager.Core.Enums;
using CarManager.Core.Extensions;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Api.DTOs.OwnerDTO
{
    public class OwnerDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(100)]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo obbligatorio")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(100)]
        public string BirthPlace { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(1)]
        public OwnerGender Gender { get; set; }
        public string GenderName => Gender.GetDescription();

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(16)]
        public string FiscalCode { get; set; } = string.Empty;
        public List<VehicleDTO.VehicleDTO>? Vehicles { get; set; }
    }
}
