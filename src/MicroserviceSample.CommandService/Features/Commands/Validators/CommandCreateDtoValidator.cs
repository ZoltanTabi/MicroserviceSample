using FluentValidation;
using MicroserviceSample.CommandService.Features.Commands.Dtos;

namespace MicroserviceSample.CommandService.Features.Commands.Validators;

public class CommandCreateDtoValidator : AbstractValidator<CommandCreateDto>
{
    public CommandCreateDtoValidator()
    {
        RuleFor(x => x.HowTo)
            .NotEmpty().WithMessage("HowTo is required.");

        RuleFor(x => x.CommandLine)
            .NotEmpty().WithMessage("CommandLine is required.");
    }
}
