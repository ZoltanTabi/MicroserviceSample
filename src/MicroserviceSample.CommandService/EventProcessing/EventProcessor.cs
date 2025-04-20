using AutoMapper;
using MicroserviceSample.CommandService.Common.Dtos;
using MicroserviceSample.CommandService.Domains;
using MicroserviceSample.CommandService.Features.Platforms.Dtos;
using MicroserviceSample.CommandService.Persistance.Repositories;
using System.Text.Json;

namespace MicroserviceSample.CommandService.EventProcessing;

public class EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper) : IEventProcessor
{
    private readonly IServiceScopeFactory scopeFactory = scopeFactory;
    private readonly IMapper mapper = mapper;

    public async Task ProcessEventAsync(string message)
    {
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.PlatformPublished:
                await AddPlatform(message);
                break;
            default:
                Console.WriteLine($"--> Could not determine event type. Message: {message}");
                break;
        }

        await Task.CompletedTask;
    }

    private static EventType DetermineEvent(string notificationMessage)
    {
        Console.WriteLine($"--> Determining event type for message: {notificationMessage}");

        var genericEventDto = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        return genericEventDto?.Event switch
        {
            "Platform_Published" => EventType.PlatformPublished,
            _ => EventType.Undetermined
        };
    }

    private async Task AddPlatform(string platformPublishedMessage)
    {
        using var scope = scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();

        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

        try
        {
            var platform = mapper.Map<Platform>(platformPublishedDto);

            if (!await repository.ExternalPlatformExistAsync(platform.ExternalId))
            {
                Console.WriteLine($"--> Platform {platform.ExternalId} is new. Adding to DB.");
                await repository.CreatePlatformAsync(platform);
            }
            else
            {
                Console.WriteLine($"--> Platform {platform.Id} already exists. Ignoring.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not check if platform exists: {ex.Message}");
        }
    }
}

enum EventType
{
    PlatformPublished,
    Undetermined
}
