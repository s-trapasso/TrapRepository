using FluentValidation;
using CarManager.Application.Models.Maintenance;
using CarManager.Core.Enums;

namespace CarManager.Application.Validators.Maintenance;

public class UpdateMaintenanceValidator : AbstractValidator<MaintenanceUpdateModel>
{
    public UpdateMaintenanceValidator()
    {
        RuleFor(x => x.VehicleId)
            .GreaterThan(0);

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Today);

        RuleFor(x => x.MaintenanceType)
            .IsInEnum()
            .NotEqual(MaintenanceType.Unknown);

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