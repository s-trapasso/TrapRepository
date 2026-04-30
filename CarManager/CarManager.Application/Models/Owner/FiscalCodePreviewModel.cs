using CarManager.Core.Enums;

namespace CarManager.Application.Models.Owner
{
    public record FiscalCodePreviewModel(
        string FirstName,
        string LastName,
        DateTime BirthDate,
        string BirthPlace,
        OwnerGender Gender);
}
