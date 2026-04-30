using CarManager.Core.Enums;
namespace CarManager.Application.Models.Owner
{
    public class OwnerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; } = string.Empty;
        public OwnerGender Gender { get; set; }          // <— enum, non string
        public string? GenderName { get; set; }          // <— valorizzato dall’API
        public string FiscalCode { get; set; } = string.Empty;
        
    }
}
