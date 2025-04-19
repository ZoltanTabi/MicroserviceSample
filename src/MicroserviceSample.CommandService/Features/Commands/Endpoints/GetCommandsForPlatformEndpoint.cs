using AutoMapper;
using MicroserviceSample.CommandService.Features.Commands.Dtos;
using MicroserviceSample.CommandService.Persistance.Repositories;

namespace MicroserviceSample.CommandService.Features.Commands.Endpoints;

public static class GetCommandsForPlatformEndpoint
{
    public static async Task<IResult> Handle(
        string platformId,
        ICommandRepository repository,
        IMapper mapper)
    {
        Console.WriteLine($"--> Getting commands for platform {platformId}");

        if (!await repository.PlatformExistAsync(platformId))
        {
            return TypedResults.NotFound();
        }

        var commands = await repository.GetCommandsFormPlatformAsync(platformId);

        return TypedResults.Ok(mapper.Map<List<CommandReadDto>>(commands));
    }
}
