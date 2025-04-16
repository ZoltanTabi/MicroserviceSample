using FluentValidation;
using MicroserviceSample.PlatformService.Features.Platforms.Dtos;

namespace MicroserviceSample.PlatformService.Features.Platforms.Validators;

public class PlatformCreateDtoValidator : AbstractValidator<PlatformCreateDto>
{
    public PlatformCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Publisher)
            .NotEmpty().WithMessage("Publisher is required.");

        RuleFor(x => x.Cost)
            .NotEmpty().WithMessage("Cost is required.");
    }
}
