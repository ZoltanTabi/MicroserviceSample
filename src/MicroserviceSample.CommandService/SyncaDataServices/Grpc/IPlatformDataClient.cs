using MicroserviceSample.CommandService.Domains;

namespace MicroserviceSample.CommandService.SyncaDataServices.Grpc;

public interface IPlatformDataClient
{
    IEnumerable<Platform> ReturnAllPlatforms();
}
