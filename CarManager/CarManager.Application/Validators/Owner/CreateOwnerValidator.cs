using FluentValidation;
using CarManager.Application.Models.Owner;
using CarManager.Application.Validators.Common;

namespace CarManager.Application.Validators.Owner;

public class CreateOwnerValidator : AbstractValidator<OwnerCreateModel>
{
    public CreateOwnerValidator()
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
            .LessThan(DateTime.Today)
            .WithMessage("La data di nascita deve essere nel passato");

        RuleFor(x => x.FiscalCode)
            .NotEmpty()
            .Length(16)
            .Matches("^[A-Z0-9]+$")
            .WithMessage("Codice fiscale non valido (solo lettere maiuscole e numeri)");
    }
}