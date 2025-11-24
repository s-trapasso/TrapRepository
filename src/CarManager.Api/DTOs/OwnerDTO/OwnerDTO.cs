using CarManager.Core.Models;
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
        public string Gender { get; set; } = string.Empty;  // "M" o "F"

        [Required(ErrorMessage = "Campo obbligatorio"), MaxLength(16)]
        public string FiscalCode { get; set; } = string.Empty;

    }
}
