using AutoMapper;
using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.CommandService.Features.Commands.Dtos;
using MicroserviceSample.CommandService.Persistance.Repositories;

namespace MicroserviceSample.CommandService.Features.Commands.Endpoints;

public static class CreateCommandEndpoint
{
    public static async Task<IResult> Handle(
        int platformId,
        CommandCreateDto commandDto,
        ICommandRepository repository,
        IMapper mapper)
    {
        if (!await repository.PlatformExistAsync(platformId))
        {
            return TypedResults.BadRequest();
        }

        var command = mapper.Map<Command>(commandDto);

        await repository.CreateCommandAsync(platformId, command);
        await repository.SaveChangesAsync();

        var commandReadDto = mapper.Map<CommandReadDto>(command);

        return TypedResults.Created($"/c/platforms/{platformId}/commands/{commandReadDto.Id}", commandReadDto);
    }
}
