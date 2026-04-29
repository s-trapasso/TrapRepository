using CarManager.Core.Enums;
using System;

namespace CarManager.App.Models.Owner
{
    public record FiscalCodePreviewModel(
        string FirstName,
        string LastName,
        DateTime BirthDate,
        string BirthPlace,
        OwnerGender Gender);
}
