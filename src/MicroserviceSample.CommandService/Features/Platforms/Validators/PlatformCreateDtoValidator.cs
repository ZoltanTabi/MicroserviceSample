using FluentValidation;
using MicroserviceSample.CommandService.Features.Platforms.Dtos;

namespace MicroserviceSample.CommandService.Features.Platforms.Validators;

public class PlatformCreateDtoValidator : AbstractValidator<PlatformCreateDto>
{
    public PlatformCreateDtoValidator()
    {
        RuleFor(x => x.ExternalId)
            .GreaterThan(0).WithMessage("ExternalId must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
}
