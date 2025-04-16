using AutoMapper;
using MicroserviceSample.PlatformService.Features.Platforms.Dtos;
using MicroserviceSample.PlatformService.Persistance.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.PlatformService.Features.Platforms.Endpoints;

public static class GetAllPlatformsEndpoint
{
    public static async Task<Ok<List<PlatformReadDto>>> Handle(
        IPlatformRepository repository,
        IMapper mapper)
    {
        var platforms = await repository.GetAllPlatformsAsync();

        return TypedResults.Ok(mapper.Map<List<PlatformReadDto>>(platforms));
    }
}
