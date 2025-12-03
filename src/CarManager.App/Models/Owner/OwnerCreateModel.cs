using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.App.Models.Owner
{
    public class OwnerCreateModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = default!;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = default!;
        [Required]
        [StringLength(100)]
        public string Address { get; set; } = default!;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(50)]
        public string BirthPlace { get; set; } = default!;
        [Required]
        [StringLength(1)]
        public string Gender { get; set; } = default!; // "M" o "F"
        [Required]
        [StringLength(16)]
        public string FiscalCode { get; set; } = default!;
    }
}
