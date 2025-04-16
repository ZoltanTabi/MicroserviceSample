using AutoMapper;
using MicroserviceSample.CommandService.Features.Commands.Dtos;
using MicroserviceSample.CommandService.Persistance.Repositories;

namespace MicroserviceSample.CommandService.Features.Commands.Endpoints;

public static class GetCommandEndpoint
{
    public static async Task<IResult> Handle(
        int platformId,
        int commandId,
        ICommandRepository repository,
        IMapper mapper)
    {
        var command = await repository.GetCommandAsync(platformId, commandId);

        if (command == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<CommandReadDto>(command));
    }
}
