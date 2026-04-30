using CarManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager.Application.Models.Owner
{
    public class OwnerUpdateModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; } = default!;
        public OwnerGender Gender { get; set; }
        public string? FiscalCode { get; set; }
    }
}
