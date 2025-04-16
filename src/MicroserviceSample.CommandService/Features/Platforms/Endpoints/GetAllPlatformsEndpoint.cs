using AutoMapper;
using MicroserviceSample.CommandService.Features.Platforms.Dtos;
using MicroserviceSample.CommandService.Persistance.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.CommandService.Features.Platforms.Endpoints;

public static class GetAllPlatformsEndpoint
{
    public static async Task<Ok<List<PlatformReadDto>>> Handle(
        ICommandRepository repository,
        IMapper mapper)
    {
        Console.WriteLine($"--> Getting platforms form CommandService");

        var platforms = await repository.GetAllPlatformsAsync();

        return TypedResults.Ok(mapper.Map<List<PlatformReadDto>>(platforms));
    }
}
