using AutoMapper;
using MicroserviceSample.PlatformService.Domains;
using MicroserviceSample.PlatformService.Persistance.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.PlatformService.Features.Platforms.Endpoints;

public static class CreatePlatformEndpoint
{
    public static async Task<Created<PlatformReadDto>> Handle(
        PlatformCreateDto platformDto,
        IPlatformRepository repository,
        IMapper mapper)
    {
        var platform = mapper.Map<Platform>(platformDto);

        await repository.CreatePlatformAsync(platform);
        await repository.SaveChangesAsync();

        return TypedResults.Created($"/platforms/{platform.Id}", mapper.Map<PlatformReadDto>(platform));
    }
}
