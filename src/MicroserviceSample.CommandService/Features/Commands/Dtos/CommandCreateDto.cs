namespace MicroserviceSample.CommandService.Features.Commands.Dtos;

public class CommandCreateDto
{
    public string HowTo { get; set; } = string.Empty;
    public string CommandLine { get; set; } = string.Empty;
}
