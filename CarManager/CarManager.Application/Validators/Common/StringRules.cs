using FluentValidation;

namespace CarManager.Application.Validators.Common;

public static class StringRules
{
    public static IRuleBuilderOptions<T, string> RequiredMax<T>(
        this IRuleBuilder<T, string> rule,
        int maxLength)
    {
        return rule
            .NotEmpty().WithMessage("Campo obbligatorio")
            .MaximumLength(maxLength).WithMessage($"Max {maxLength} caratteri");
    }
}