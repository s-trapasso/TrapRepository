using FluentValidation;
using CarManager.Application.Models.Vehicle;
using CarManager.Application.Validators.Common;
using CarManager.Core.Enums;

namespace CarManager.Application.Validators.Vehicle;

public class CreateVehicleValidator : AbstractValidator<VehicleCreateModel>
{
    public CreateVehicleValidator()
    {
        RuleFor(x => x.Plate).RequiredMax(20);
        RuleFor(x => x.Brand).RequiredMax(100);
        RuleFor(x => x.Model).RequiredMax(100);

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.Now.Year + 1);

        RuleFor(x => x.Km)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.FuelType)
            .IsInEnum()
            .NotEqual(FuelTypeEnum.Unknown);
    }
}