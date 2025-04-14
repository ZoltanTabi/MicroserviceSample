using MicroserviceSample.PlatformService.Features.Platforms;

namespace MicroserviceSample.PlatformService.SyncDataServices.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto platformReadDto);
}
