using AutoMapper;
using MicroserviceSample.PlatformService.Persistance.Repositories;

namespace MicroserviceSample.PlatformService.Features.Platforms.Endpoints;

public static class GetPlatformEndpoint
{
    public static async Task<IResult> Handle(
        int id,
        IPlatformRepository repository,
        IMapper mapper)
    {
        var platform = await repository.GetPlatformByIdAsync(id);

        if (platform is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<PlatformReadDto>(platform));
    }
}
