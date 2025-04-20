using MicroserviceSample.PlatformService.Features.Platforms.Dtos;

namespace MicroserviceSample.PlatformService.AsyncDataServices;

public interface IMessageBusClient
{
    Task PublishNewPlatformAsync(PlatformPublishedDto platformPublishedDto);
}
