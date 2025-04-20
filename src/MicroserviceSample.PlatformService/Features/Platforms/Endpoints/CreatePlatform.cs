using AutoMapper;
using MicroserviceSample.PlatformService.AsyncDataServices;
using MicroserviceSample.PlatformService.Domains;
using MicroserviceSample.PlatformService.Features.Platforms.Dtos;
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
        IMessageBusClient messageBusClient,
        IMapper mapper)
    {
        var platform = mapper.Map<Platform>(platformDto);

        await repository.CreatePlatformAsync(platform);
        await repository.SaveChangesAsync();

        var platformReadDto = mapper.Map<PlatformReadDto>(platform);

        // Send synchronously to CommandService
        try
        {
            await commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously to CommandService: {ex.Message}");
        }

        // Send asynchronously to CommandService
        try
        {
            var platformPublishedDto = mapper.Map<PlatformPublishedDto>(platformReadDto);
            platformPublishedDto.Event = "Platform_Published";

            await messageBusClient.PublishNewPlatformAsync(platformPublishedDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send asynchronously to CommandService: {ex.Message}");
        }

        return TypedResults.Created($"/platforms/{platformReadDto.Id}", platformReadDto);
    }
}
