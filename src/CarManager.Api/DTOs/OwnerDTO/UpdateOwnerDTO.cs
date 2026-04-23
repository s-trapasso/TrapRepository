using CarManager.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarManager.Api.DTOs.OwnerDTO
{
    public class UpdateOwnerDTO
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; } = default!;

        [Required, StringLength(50)]
        public string LastName { get; set; } = default!;

        [Required, StringLength(100)]
        public string Address { get; set; } = default!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(50)]
        public string BirthPlace { get; set; } = default!;

        [Required]
        public OwnerGender Gender { get; set; }

        [Required, StringLength(16)]
        public string FiscalCode { get; set; } = default!;
    }
}
