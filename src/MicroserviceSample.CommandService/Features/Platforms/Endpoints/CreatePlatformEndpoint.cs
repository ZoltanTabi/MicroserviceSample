using AutoMapper;
using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.CommandService.Features.Platforms.Dtos;
using MicroserviceSample.CommandService.Persistance.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.CommandService.Features.Platforms.Endpoints;

public static class CreatePlatformEndpoint
{
    public static async Task<Created<PlatformReadDto>> Handle(
        PlatformCreateDto platformDto,
        ICommandRepository repository,
        IMapper mapper)
    {
        var platform = mapper.Map<Platform>(platformDto);

        await repository.CreatePlatformAsync(platform);

        var platformReadDto = mapper.Map<PlatformReadDto>(platform);

        return TypedResults.Created($"/platforms/{platformReadDto.Id}", platformReadDto);
    }
}
