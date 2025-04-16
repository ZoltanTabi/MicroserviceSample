namespace MicroserviceSample.CommandService.Features.Platforms.Dtos;

public class PlatformCreateDto
{
    public int ExternalId { get; set; }
    public string Name { get; set; } = string.Empty;
}
