using CarManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Api.DTOs.Owner
{
    public class FiscalCodePreviewDTO
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; } = default!;

        [Required, StringLength(50)]
        public string LastName { get; set; } = default!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(50)]
        public string BirthPlace { get; set; } = default!;

        [Required]
        public OwnerGender Gender { get; set; }
    }
}