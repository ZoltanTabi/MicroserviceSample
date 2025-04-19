namespace MicroserviceSample.CommandService.Features.Commands.Dtos;

public class CommandReadDto
{
    public string Id { get; set; } = string.Empty;
    public string HowTo { get; set; } = string.Empty;
    public string CommandLine { get; set; } = string.Empty;
    public string PlatformId { get; set; } = string.Empty;
}
