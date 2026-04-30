using FluentValidation;
using CarManager.Application.Models.Owner;
using CarManager.Application.Validators.Common;

namespace CarManager.Application.Validators.Owner;

public class UpdateOwnerValidator : AbstractValidator<OwnerUpdateModel>
{
    public UpdateOwnerValidator()
    {
        RuleFor(x => x.FirstName)
            .RequiredMax(100);

        RuleFor(x => x.LastName)
            .RequiredMax(100);

        RuleFor(x => x.Address)
            .RequiredMax(200);

        RuleFor(x => x.BirthPlace)
            .RequiredMax(100);

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Today);

        RuleFor(x => x.FiscalCode)
            .NotEmpty()
            .Length(16)
            .Matches("^[A-Z0-9]+$");
    }
}