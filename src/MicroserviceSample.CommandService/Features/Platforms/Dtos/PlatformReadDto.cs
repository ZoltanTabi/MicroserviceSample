namespace MicroserviceSample.CommandService.Features.Platforms.Dtos;

public class PlatformReadDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int ExternalId { get; set; }
}
