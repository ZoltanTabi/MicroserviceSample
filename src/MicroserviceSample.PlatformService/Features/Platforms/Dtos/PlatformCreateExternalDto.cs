namespace MicroserviceSample.PlatformService.Features.Platforms.Dtos;

public class PlatformCreateExternalDto
{
    public int ExternalId { get; set; }
    public string Name { get; set; } = string.Empty;
}
