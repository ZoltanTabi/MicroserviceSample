using AutoMapper;
using MicroserviceSample.PlatformService.Domains;
using MicroserviceSample.PlatformService.Persistance.Repositories;
using MicroserviceSample.PlatformService.SyncDataServices.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MicroserviceSample.PlatformService.Features.Platforms.Endpoints;

public static class CreatePlatformEndpoint
{
    public static async Task<Created<PlatformReadDto>> Handle(
        PlatformCreateDto platformDto,
        IPlatformRepository repository,
        ICommandDataClient commandDataClient,
        IMapper mapper)
    {
        var platform = mapper.Map<Platform>(platformDto);

        await repository.CreatePlatformAsync(platform);
        await repository.SaveChangesAsync();

        var platformReadDto = mapper.Map<PlatformReadDto>(platform);

        try
        {
            await commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously to CommandService: {ex.Message}");
        }

        return TypedResults.Created($"/platforms/{platformReadDto.Id}", platformReadDto);
    }
}
