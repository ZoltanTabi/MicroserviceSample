using Microsoft.AspNetCore.Http.HttpResults;
using MicroserviceSample.PlatformService.Features.Platforms.Endpoints;
using MicroserviceSample.PlatformService.Features.Platforms.Dtos;
using MicroserviceSample.PlatformService.Common.Filters;

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
            .AddEndpointFilter<ValidationFilter<PlatformCreateDto>>()
            .Produces<Created<PlatformReadDto>>()
            .Produces<BadRequest>();
    }
}
