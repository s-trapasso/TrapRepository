using FluentValidation;
using CarManager.Application.Models.Maintenance;
using CarManager.Core.Enums;

namespace CarManager.Application.Validators.Maintenance;

public class CreateMaintenanceValidator : AbstractValidator<MaintenanceCreateModel>
{
    public CreateMaintenanceValidator()
    {
        RuleFor(x => x.VehicleId)
            .GreaterThan(0)
            .WithMessage("Veicolo obbligatorio");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("La data non può essere nel futuro");

        RuleFor(x => x.MaintenanceType)
            .IsInEnum()
            .NotEqual(MaintenanceType.Unknown)
            .WithMessage("Tipo manutenzione obbligatorio");

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Km)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Km.HasValue);

        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Cost.HasValue);

        RuleFor(x => x.Workshop)
            .MaximumLength(200);

        RuleFor(x => x.Notes)
            .MaximumLength(1000);
    }
}