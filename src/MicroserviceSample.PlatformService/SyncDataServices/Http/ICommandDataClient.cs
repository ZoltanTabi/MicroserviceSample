using MicroserviceSample.PlatformService.Features.Platforms.Dtos;

namespace MicroserviceSample.PlatformService.SyncDataServices.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto platformReadDto);
}
