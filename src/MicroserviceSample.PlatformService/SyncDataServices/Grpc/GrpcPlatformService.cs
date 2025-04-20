using AutoMapper;
using Grpc.Core;
using MicroserviceSample.PlatformService.Persistance.Repositories;

namespace MicroserviceSample.PlatformService.SyncDataServices.Grpc;

public class GrpcPlatformService(IPlatformRepository platformRepository, IMapper mapper) : GrpcPlatform.GrpcPlatformBase
{
    private readonly IPlatformRepository platformRepository = platformRepository;
    private readonly IMapper mapper = mapper;

    public override async Task<GetAllResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        Console.WriteLine($"--> Getting platforms from PlatformService");
        var platforms = await platformRepository.GetAllPlatformsAsync();

        return new GetAllResponse
        {
            Platforms =
            {
                mapper.Map<List<GrpcPlatformModel>>(platforms)
            }
        };
    }
}
