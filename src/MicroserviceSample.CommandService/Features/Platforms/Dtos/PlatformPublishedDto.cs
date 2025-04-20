using MicroserviceSample.CommandService.Common.Dtos;

namespace MicroserviceSample.CommandService.Features.Platforms.Dtos;

public class PlatformPublishedDto : GenericEventDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
