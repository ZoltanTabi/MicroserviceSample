using Microsoft.AspNetCore.Http.HttpResults;
using MicroserviceSample.PlatformService.Features.Platforms.Endpoints;

namespace MicroserviceSample.PlatformService.Features.Platforms;

public static class PlatformEndpoints
{
    public static void MapPlatformEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder platforms = app.MapGroup("/platforms");

        platforms.MapGet("/", GetAllPlatformsEndpoint.Handle)
            .WithName("GetAllPlatforms")
            .Produces<Ok<List<PlatformReadDto>>>();

        platforms.MapGet("/{id:int}", GetPlatformEndpoint.Handle)
            .WithName("GetPlatform")
            .Produces<Ok<PlatformReadDto>>()
            .Produces<NotFound>();

        platforms.MapPost("/", CreatePlatformEndpoint.Handle)
            .WithName("CreatePlatform")
            .Produces<Created<PlatformReadDto>>()
            .Produces<BadRequest>();
    }
}
