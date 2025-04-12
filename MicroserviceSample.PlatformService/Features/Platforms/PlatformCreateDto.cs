namespace MicroserviceSample.PlatformService.Features.Platforms;

public class PlatformCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
}
