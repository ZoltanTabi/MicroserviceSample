namespace MicroserviceSample.CommandService.Features.Platforms.Dtos;

public class PlatformReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ExternalId { get; set; } = string.Empty;
}
